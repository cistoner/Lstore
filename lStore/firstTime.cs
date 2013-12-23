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

namespace lStore
{
    public partial class firstTime : Form
    {
        public int step = 1;

        /**
         * basic URL for pinging all data
         */ 
        public string url = "http://VAIO/cistoner/q4sp9x/lstore/";
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
        public static int netStepCount = 10;

        private bool[] isFileDownloaded = new bool[firstTime.netStepCount];
        public firstTime()
        {
            InitializeComponent();
            this.BringToFront();
            localSyncLabel.Visible = false;
            
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
            while (step <= firstTime.netStepCount)
            {
                string tmp = SendPost(url +"getdata", "hash=" +hash +"&step=" + step.ToString());
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
            progress.Value += e.ProgressPercentage;
            if (!isFirst) 
            {
                isFirst = true;
                localSyncLabel.Visible = true;
                localsync.RunWorkerAsync();
            }
            if (step <= 10)
            {
                stepCount.Text = " " + step.ToString() + " of " +firstTime.netStepCount.ToString();
            }
            else {
                stepCount.Text = firstTime.netStepCount.ToString() +" of " + firstTime.netStepCount.ToString();
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
                while (!File.Exists(primaryFolder + @"\tmp\data_step_" + count.ToString() + ".data") ) 
                {
                    /**
                     * makes current thread sleep for 400ms
                     */ 
                    System.Threading.Thread.Sleep(400);

                };
                //this waits till the file has been created

                while (!isFileDownloaded[count - 1]) 
                {
                    /**
                     * makes current thread sleep for 400ms
                     */
                    System.Threading.Thread.Sleep(400);
                };
                //this makes code wait till data has been completely saved

                string[] filesRows = File.ReadAllLines(primaryFolder + @"\tmp\data_step_" + count.ToString() + ".data");
                
                /**
                 * assuming the rows be of format 
                 * id - filename - keyword - rating NEWLINE
                 */
                SqlConnection mycon = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\q4sp9x.mdf;Integrated Security=True;Connect Timeout=30");
                var command = mycon.CreateCommand();    
                mycon.Open();
                for (int i = 0; i < filesRows.Length; i++)
                {
                    string[] arr = filesRows[i].Split('*');
                    try
                    {
                        command.CommandText = "INSERT INTO files (Id,filename,keywords,rating) VALUES (@param1,@param2,@param3,@param4)";
                        command.Parameters.AddWithValue("param1", int.Parse(arr[0]));
                        command.Parameters.AddWithValue("param2", arr[1]);
                        command.Parameters.AddWithValue("param3", arr[2]);
                        command.Parameters.AddWithValue("param4", float.Parse(arr[3]));
                        MessageBox.Show(command.CommandType.ToString());
                        command.ExecuteNonQuery();
                        mycon.Close();                        
                    }
                    catch (Exception ex)
                    {
                       /**
                        * these exception better be logged than shown directly
                        * you might need to parse these exception in some sort and
                        * make sure things work!
                        */
                        File.AppendAllText(primaryFolder +@"\sync.log",ex.Message +Environment.NewLine);
                    } 
                }
                localsync.ReportProgress(5);
                count++;
            }

            /**
             * this will be sent when local sync is over that means its time 
             * to show finish button 
             */ 
            localsync.ReportProgress(0);
            
        }
        private void localsync_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                finishbutton.Visible = true;
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
