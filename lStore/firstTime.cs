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
        public string url = "http://www.cistoner.com/abcd/sync.php";
        public bool isFirst = false;
        private bool[] isFileDownloaded = new bool[10];
        public string primaryFolder = @"C:\Users\" + Environment.UserName + @"\Documents\lStore";
        public firstTime()
        {
            InitializeComponent();
            localSyncLabel.Visible = false;
            stepCount.Text = " 1 of 10 ";
            for (int i = 0; i < 10; i++)
            {
                isFileDownloaded[i] = false;    //for remoiving exceptio
            }
            onlinesync.RunWorkerAsync();
            
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
                onlinesync.ReportProgress(step * 5);
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
            stepCount.Text = " " +step.ToString() + " of 10 ";
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
                while (!File.Exists(primaryFolder + @"\tmp\data_step_" + count.ToString() + ".data") && isFileDownloaded[count-1]) { };
                //this waits till the file has been created
                
                string[] files = File.ReadAllLines(primaryFolder + @"\tmp\data_step_" + count.ToString() + ".data");
                for (int i = 0; i < files.Length; i++)
                { 
                    /* db query to
                     * save this content to local db
                     */ 
                }
                localsync.ReportProgress(count * 5);
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

        

        

        

        

        

        
    }
}
