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

namespace lStore
{
    public partial class firstTime : Form
    {
        public int step = 1;
        public string url = "http://localhost/lstore/";
        public bool isFirst = false,isProxyEnabled = false;
        private bool[] isFileDownloaded = new bool[10];
        public string primaryFolder = @"C:\Users\" + Environment.UserName + @"\Documents\lStore";
        private bool isDataSyncOver = false, isUserListRetrieved = false;   //these two variables are flag for two imp processes
        private bool isInternetActivated = false;
        public firstTime()
        {
            InitializeComponent();
            localSyncLabel.Visible = false;
            if (canConnectTourl())
            {
                stepCount.Text = " 1 of 10 ";
                for (int i = 0; i < 10; i++)
                {
                    isFileDownloaded[i] = false;    //for remoiving exceptio
                }
                isInternetActivated = true;
            }
            else 
            {
                isProxyEnabled = true;
                proxyLabel.Text = "PROXY: Enabled";
                proxyLabel.ForeColor = System.Drawing.Color.Red;
                if (canConnectTourl())
                {
                    stepCount.Text = " 1 of 10 ";
                    for (int i = 0; i < 10; i++)
                    {
                        isFileDownloaded[i] = false;    //for remoiving exceptio
                    }
                    isInternetActivated = true;
                }
                else
                {
                    proxyLabel.Text = "Unable to connect! you may have to use proxifiers like \" Ultra surf \""; 
                }
                           
            }
            if (isInternetActivated)
            { 
                /* 
                 * all internet related process here
                 * 
                 */
                onlinesync.RunWorkerAsync();
                userListRetriever.RunWorkerAsync();
            }
            
            
        }
        /* 
         * function to check if program can connect
         * to required server
         */ 
        private bool canConnectTourl()
        {
            string data = SendPost(url, "");
            if ( data.Length != 0 ) return true;
            return false;
        }
        /* 
         * main working fnction of
         * backgroundworker that downloads data from server
         */
        private void onlinesync_DoWork(object sender, DoWorkEventArgs e)
        {
            while (step <= 10)
            {
                string tmp = SendPost(url, "step=" + step.ToString());
                File.WriteAllText(primaryFolder + @"\tmp\data_step_" + step.ToString() +".data", tmp);
                isFileDownloaded[step - 1] = true;
                onlinesync.ReportProgress(5);
                step++;
            }
        }
        private void onlinesync_ProgressChanged(object sender, ProgressChangedEventArgs e)
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
                stepCount.Text = " " + step.ToString() + " of 10 ";
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
                while (!File.Exists(primaryFolder + @"\tmp\data_step_" + count.ToString() + ".data") ) { };
                //this waits till the file has been created
                while (!isFileDownloaded[count - 1]) { };
                //this makes code wait till data has been completely saved
                string[] files = File.ReadAllLines(primaryFolder + @"\tmp\data_step_" + count.ToString() + ".data");
                for (int i = 0; i < files.Length; i++)
                { 
                    /* db query to
                     * save this content to local db
                     */ 
                }
                localsync.ReportProgress(5);
                count++;
            }
            
        }
        private void localsync_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
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
            /* 
             * informartion class is required
             */
            userInfo.getAllData();  /* to make sure the userinfo class is totally initialised */
            string postdata = "";
            postdata += "dg=" + userInfo.defaultGateway + "&";
            postdata += "ma=" + userInfo.macAddress + "&";
            postdata += "username=" + userInfo.username + "&";
            postdata += "nname=" + userInfo.networkname + "&";
            postdata += "ram=" + userInfo.ram + "&";
            postdata += "cores=" + userInfo.cores + "&";
            postdata += "resolution=" + userInfo.resolution + "&";
            postdata += "os=" + userInfo.osInfo;
            string data = SendPost(url +"saveuserdata.php",postdata);
            File.WriteAllText(primaryFolder +@"savedfile.xml",data);
        }
        private void infoSender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            isDataSyncOver = true;
            if (isUserListRetrieved) 
            {
                finishbutton.Visible = true;
            }
        }
        /*
         * task is to sendrequest to server to retrieve 
         * list of all users on LAN
         * which shall be later used to 
         * search who is online and for rating purposes
         */ 
        private void userListRetriever_DoWork(object sender, DoWorkEventArgs e)
        {
            string testauthcode = "testauthcode";
            string userList = SendPost(url + "getUserList.php", "authkey=" + testauthcode);
            File.WriteAllText(primaryFolder +@"\tmp\tmpOnlineUserData.data",userList);
        }

        private void userListRetriever_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            isUserListRetrieved = true;
            /* run the code and make a custom userlist file */
            string [] data = File.ReadAllLines(primaryFolder + @"\tmp\tmpOnlineUserData.data");
            string tmp = "";
            for (int i = 0; i < data.Length; i++)
            {
                string[] user = data[i].Split('*');
                tmp += user[0] + "*";
            }
            File.WriteAllText(primaryFolder + @"\tmp\alluserlist.data",tmp);
            if (isDataSyncOver)
            {
                finishbutton.Visible = true;
            }
        }

        

        

        

        

        

        

        
    }
}
