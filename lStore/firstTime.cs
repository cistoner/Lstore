using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Collections;

namespace lStore
{
    public partial class firstTime : Form
    {
        public int step = 1;

        /**
         * basic URL for pinging all data
         */
        public string url = "http://VAIO/cistoner/q4sp9x/lstore/";
        //public string url = "http://localhost/lstore/";
        public bool isFirst = false,isProxyEnabled = false;
        public string primaryFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +@"\lStore";
        private bool isDataSyncOver = false, isLocalSyncOver = false;   //these two variables are flag for two imp processes
        private bool isInternetActivated = false;
        public string lastError = string.Empty;

        /**
         * this variable let us decide total no timeouts for connection
         */ 
        public static int maxTimeouts = 50;
        public int timeouts;
        
        /**
         * this variables represents the total no of steps in which the data
         * will be synced with server
         */

        /**
         * id of the default gateway which whill be recieved from server and saved to local db
         */ 
        private int default_gatewayID = 0;

        /**
         * id of the last file that was synced
         * btw server and local system
         */ 
        private int lastId = -1;

        public static int netStepCount = 10;

        private bool[] isFileDownloaded = new bool[firstTime.netStepCount];
        public firstTime()
        {
            InitializeComponent();
            this.BringToFront();
            localSyncLabel.Visible = false;
            
            /**
             * these are files that help us to 
             * insert uninserted data to local db
             */ 
            File.WriteAllText(primaryFolder + @"\tmp\notinserted.data", "");
            File.WriteAllText(primaryFolder + @"\tmp\sync.log", "");
            /**
             * states the no of connection timeouts that have occured
             */ 
            timeouts = 0;

            if (canConnectTourl())
            {
                /**
                 * for the case the that system can connect to 
                 * server without proxy
                 */ 
                stepCount.Text = " 1 of 10 ";
                for (int i = 0; i < 10; i++)
                {
                    isFileDownloaded[i] = false;    //for remoiving exceptio
                }
                isInternetActivated = true;
                proxyLabel.Visible = false;
            }
            else 
            {
                /**
                 * if system is not able to connect directly it will attempt a
                 * proxy connection
                 */ 
                isProxyEnabled = true;
                proxyLabel.Text = "PROXY: Enabled";
                proxyLabel.ForeColor = System.Drawing.Color.Red;
                if (canConnectTourl())
                {
                    stepCount.Text = " 1 of 10 ";
                    for (int i = 0; i < 10; i++)
                    {
                        isFileDownloaded[i] = false;    //for remoiving exceptions
                    }
                    isInternetActivated = true;
                }
                else
                {
                    
                    /**
                     * this means system is unable to connect with either
                     * normal method or proxy method
                     */ 
                    proxyLabel.Text = "Unable to connect! you may have to use proxifiers like \" Ultra surf \"";
                    hideUIElements();
                    buttonExit.Visible = true;
                    //MessageBox.Show("Unable to connect! you may have to use proxifiers like \" Ultra surf \"");
                    //Application.Exit();
                }
                           
            }
            if (isInternetActivated)
            { 
                
                /** 
                 * all internet related process here
                 * in parallel threads
                 */

                /**
                 * sending information about the user to server
                 */
                localSyncLabel.Text = "Registerting with server";
                localSyncLabel.Visible = true;
                infoSender.RunWorkerAsync();  
            }

            
        }

        /**
         * function to hide few UI items in time of fatal error
         */
        private void hideUIElements()
        {
            progress.Visible = false;
            label2.Visible = false;
            label1.Visible = false;
            stepCount.Visible = false;
            label3.Visible = false;
        }

        /* 
         * function to check if program can connect
         * to required server
         * might need to modifiy this
         */ 
        private bool canConnectTourl()
        {
            string data = SendPost(url +"index.php", "");
            if ( data.Length != 0 ) return true;
            return false;
        }

        /* 
         * main working fnction of
         * backgroundworker that downloads data from server
         */
        private void onlinesync_DoWork(object sender, DoWorkEventArgs e)
        {
            string hash = null;
            try
            {
                hash = userInfo.hash;
            }
            catch(Exception ex)
            {
                /**
                 * exception when client is not able to get HASH from XML
                 * current exception handeling not implemented
                 * need to test for this exception to occur
                 */
                lastError = ex.Message;
                onlinesync.ReportProgress(0);

                Application.Exit();
            }

            userInfo.getAllData();
            /**
             * need to send the id of last file that was updated
             */ 
            while (step <= firstTime.netStepCount)
            {
                string tmp = SendPost(url +"getdata.php", "id=" +lastId +"&hash=" +userInfo.hash +"&step=" + (step-1).ToString());
                try
                {
                    if (timeouts > firstTime.maxTimeouts)
                    {
                        /**
                         * sending a zero means telling the listner that there is timeout error
                         * this means time to exit
                         */
                        lastError = "Ehh lStore could not connect to server, seems like there is some network error :( ... \nWe tried more than 30 times. \nApplication will now exit!";
                        onlinesync.ReportProgress(0);
                    }

                    File.WriteAllText(primaryFolder + @"\tmp\data_step_" + step.ToString() + ".data", tmp);
                    isFileDownloaded[step - 1] = true;
                    onlinesync.ReportProgress((50/firstTime.netStepCount));
                    step++;
                }
                catch(Exception ex)
                {
                    /**
                     * in case the form cannot download the data correctly 
                     * this has to be downloaded again
                     */
                    timeouts++;
                    continue;
                }                    
            }
        }
        private void onlinesync_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                /**
                 * for some error for which the system cannot proceed any further this
                 * method is implemented so that user has to try again
                 */
                if (lastError.Length != 0) MessageBox.Show(lastError);
                else MessageBox.Show("OOps some error occured! Application will now exit!");

                /**
                 * code to delete the savedfile.xml so that comlete process starts again
                 */
                File.Delete(primaryFolder + @"\savedfile.xml");

                Application.Exit();
            }
            else
            {
                progress.Value += e.ProgressPercentage;
                if (!isFirst)
                {
                    isFirst = true;
                    localSyncLabel.Visible = true;
                    localsync.RunWorkerAsync();
                }
                if (step <= 10)
                {
                    stepCount.Text = " " + step.ToString() + " of " + firstTime.netStepCount.ToString();
                }
                else
                {
                    stepCount.Text = firstTime.netStepCount.ToString() + " of " + firstTime.netStepCount.ToString();
                    localSyncLabel.Text = "Saving to local system";
                }
            }
        }

        /*
         * background worker to read data line by line from file
         * and save that to db
         */
        private void localsync_DoWork(object sender, DoWorkEventArgs e)
        {
            int count = 1;
            while (count <= 10)
            {
                while (!File.Exists(primaryFolder + @"\tmp\data_step_" + count.ToString() + ".data")) 
                {
                    /**
                     * makes current thread sleep for 400ms
                     */ 
                    System.Threading.Thread.Sleep(100);

                };
                //this waits till the file has been created
                
                while (!isFileDownloaded[count - 1]) 
                {
                    /**
                     * makes current thread sleep for 400ms
                     */
                    System.Threading.Thread.Sleep(100);
                };
                
                //this makes code wait till data has been completely saved
                string[] filesRows = File.ReadAllLines(primaryFolder + @"\tmp\data_step_" + count.ToString() + ".data");

                /**
                 * assuming the rows be of format 
                 * id - filename - keyword - rating NEWLINE
                 */
                int attempts = 0;
                SqlConnection mycon = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\q4sp9x.mdf;Integrated Security=True;Connect Timeout=30");
                var command = mycon.CreateCommand();    
                mycon.Open();

                /**
                 * code to retrieve the default gateway ID shall be implemented outside the loop
                 */
                if (count == 1)
                {
                    string tempstring = filesRows[0].Replace("-_-", @"\");
                    string[] tmparr = tempstring.Split('\\');
                    string defaultGateway = tmparr[1];
                    command.CommandText = "SELECT Id FROM defaultGateway WHERE IP = @param";
                    command.Parameters.AddWithValue("param", defaultGateway);
                    SqlDataReader r = command.ExecuteReader();
                    if (r.HasRows)
                    {
                        while (r.Read())
                        {
                            default_gatewayID = int.Parse(r["id"].ToString());
                            break;  //expecting only one row
                        }
                    }
                    else
                    {
                        /**
                         * this means no data gateway exists for this default gateway
                         * so we need to enter it and retrieve data
                         */
                        mycon.Close();
                        mycon.Open();
                        command.Dispose();
                        command.CommandText = "INSERT INTO defaultGateway(IP) VALUES (@ip)";
                        command.Parameters.AddWithValue("ip", defaultGateway);
                        int countAffectedRows = command.ExecuteNonQuery();    //data inserted
                        if (countAffectedRows != 1)
                        {
                            /**
                             * this is an error condition as this means insertion of no item
                             * need to specify somethin in here
                             */
                        }
                        /**
                         * insertion done : now retrieve ID again
                         */
                        command.CommandText = "SELECT Id FROM defaultGateway WHERE IP = @IP_";
                        command.Parameters.AddWithValue("IP_", defaultGateway);
                        r = command.ExecuteReader();
                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                default_gatewayID = int.Parse(r["id"].ToString());
                                break;  //expecting only one row
                            }
                        }
                        else
                        {
                            /**
                             * this means unable to insert error
                             * so we need to show error and exit
                             */
                            lastError = "ERROR in LOCAL DB SYNC UNABLE TO GET default gateway data from database";
                            localsync.ReportProgress(0);
                        }
                        mycon.Close();

                    }
                }
                /**
                 * code to retrieve the default gateway ID ends
                 */
                try
                {
                    mycon.Open();
                }
                catch (Exception ex) { }
                for (int i = 0; i < filesRows.Length;i++ )
                {
                    filesRows[i] = filesRows[i].Replace("-_-",@"\");
                    string[] arr = filesRows[i].Split('\\'); 
                    try
                    {
                        /**
                         * note here: presently different @param scalars were created but
                         * resarch about using only one @param scalar for each insertion
                         */ 
                        command.CommandText = "INSERT INTO files (Id,filename,keywords,dg_id) VALUES (@param" + i.ToString() + "1,@param" + i.ToString() + "2,@param" + i.ToString() + "3,@param" + i.ToString() + "4)";
                        command.Parameters.AddWithValue("param" + i.ToString() + "1", int.Parse(arr[0]));       //file id
                        command.Parameters.AddWithValue("param" + i.ToString() + "2", arr[2]);                   //filename
                        command.Parameters.AddWithValue("param" + i.ToString() + "3", arr[3].ToString());                  //keywords
                        command.Parameters.AddWithValue("param" + i.ToString() + "4", default_gatewayID);
                        int countrow = command.ExecuteNonQuery();
                        if (countrow != 1)
                        {
                            File.AppendAllText(primaryFolder + @"\steps.txt", "INSERTION FAILED FOR ELEMENT_" + i.ToString() + Environment.NewLine);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                       /**
                        * these exception better be logged than shown directly
                        * you might need to parse these exception in some sort and
                        * make sure things work!
                        */
                        File.AppendAllText(primaryFolder + @"\tmp\notinserted.data", filesRows[i] + Environment.NewLine);
                        if (ex.Message.IndexOf("UQ__files_") == -1)
                        {
                            File.AppendAllText(primaryFolder + @"\tmp\sync.log", ex.Message + Environment.NewLine);
                        }
                    } 
                }
                mycon.Close(); 
                localsync.ReportProgress(5);
                count++;
            }

            /**
             * this will be sent when local sync is over that means its time 
             * to show finish button 
             */ 
            
            
        }
        private void localsync_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                MessageBox.Show(lastError);
            }
            progress.Value += e.ProgressPercentage;
        }
        private void localsync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /* 
             * this means local syncing is over 
             * by this time configuration files should 
             * have been made
             */
            finishbutton.Visible = true;
            localSyncLabel.Visible = false;
        }

        /*
         * function to send post request to any page and retrieve the content
         */ 
        public string SendPost(string url, string postData)
        {
            string webpageContent = string.Empty;

            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                if (isProxyEnabled)
                {
                    /* 
                     * to enable proxy connection
                     */ 
                    WebProxy myproxy = new WebProxy("127.0.0.1", 9666);
                    myproxy.BypassProxyOnLocal = false;
                    webRequest.Proxy = myproxy;
                }
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;

                using (Stream webpageStream = webRequest.GetRequestStream())
                {
                    webpageStream.Write(byteArray, 0, byteArray.Length);
                }

                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        webpageContent = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                //throw or return an appropriate response/exception
            }
            return webpageContent;
        }

        private void infoSender_DoWork(object sender, DoWorkEventArgs e)
        {
            /** 
             * informartion class is required
             */
            userInfo.getAllData();  

            /** 
             * to make sure the userinfo class is totally initialised 
             */
            string postdata = "";
            postdata += "dg=" + userInfo.defaultGateway + "&";
            postdata += "ma=" + userInfo.macAddress + "&";
            postdata += "username=" + userInfo.username + "&";
            postdata += "nname=" + userInfo.networkname + "&";
            postdata += "ram=" + userInfo.ram + "&";
            postdata += "cores=" + userInfo.cores + "&";
            postdata += "resolution=" + userInfo.resolution + "&";
            postdata += "os=" + userInfo.osInfo;
            string data = SendPost(url +"register.php",postdata);
            if (data == "1003")
            {
                /**
                 * if this error is prone to happen implement a mechanism to send the feedback immediately to server so that we can
                 * have an inforamtion about this anytime
                 */ 
                lastError = "Unable to send complete information. Check your network connection! Application will now exit.";
                infoSender.ReportProgress(0);

                /** 
                 * function call to send feedback asap
                 */ 

                Application.Exit();
            }
            else if (data == "1049")
            {
                /**
                 * if this error is prone to happen implement a mechanism to send the feedback immediately to server so that we can
                 * have an inforamtion about this anytime
                 */
                lastError = "Well this ain't cool! our server encountered some error and we have reported this issue to administratiors. Give us some time to solve this and restart this app in 2hours. Thanks!!";
                infoSender.ReportProgress(0);

                /** 
                 * function call to send feedback asap
                 */

                
            }
            File.WriteAllText(primaryFolder +@"\savedfile.xml",data);
            addDataTODB();
        }

        /**
         * function to add these details to database as well
         */
        private void addDataTODB()
        {
            using (SqlConnection mycon = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\q4sp9x.mdf;Integrated Security=True;Connect Timeout=300"))
            {
                try
                {
                    string sqlSelect = "INSERT INTO dbo.userInfo(uname,nname,dg,ma) VALUES (@param1,@param2,@param3,@param4)";
                    SqlCommand sqlC = new SqlCommand(sqlSelect, mycon);
                    sqlC.Parameters.Add(new SqlParameter("param1", userInfo.username.ToString()));
                    sqlC.Parameters.Add(new SqlParameter("param2", userInfo.networkname.ToString()));
                    sqlC.Parameters.Add(new SqlParameter("param3", userInfo.defaultGateway.ToString()));
                    sqlC.Parameters.Add(new SqlParameter("param4", userInfo.macAddress.ToString()));
                    //sqlC.Parameters.Add(new SqlParameter("param5", float.Parse(userInfo.rating)));
                    //sqlC.Parameters.Add(new SqlParameter("param6", userInfo.location.ToString()));
                    //sqlC.Parameters.Add(new SqlParameter("param7", int.Parse(userInfo.files_shared)));
                    mycon.Open();
                    int count = sqlC.ExecuteNonQuery();
                    if (count == 0)
                    {
                        /**
                         * this means information was not feeded to local db
                         * and we need to correct it so for now logging should be done
                         */ 
                    }
                    mycon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SQL ERROR: " +ex.Message);
                }
                
            }
        }

        private void infoSender_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                MessageBox.Show(lastError);
                Application.Exit();
            }
        }

        private void infoSender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /**
             * once user has been register now data has to be sent to client
             */
            localSyncLabel.Text = "Synchronysing...";
            onlinesync.RunWorkerAsync();

            /*
            isDataSyncOver = true;
            if (isUserListRetrieved) 
            {
                finishbutton.Visible = true;
            }
            */
        }

        
        /* 
         * function to restart app on clicking
         * on restart button
         */ 
        private void finishbutton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Woohoo! lStore has synced data with server! Application shall restart now");
            Application.Restart();
        }

        private void firstTime_Load(object sender, EventArgs e)
        {

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        

        

        

        

        

        

        
    }
}
