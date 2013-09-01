using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;
using System.Xml;
using System.Threading;
//=========================================namespaces till here==============
namespace lStore
{
    public partial class lStore : Form
    {
        //=====variables here===================
        public string userName = Environment.UserName, localName;
        public string primaryFolder;
        public lStore()
        {
            InitializeComponent();
            localName = System.Environment.MachineName;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            /*task here is to check if this is first time or not
            this is the first task to be performed by the tool in main thread
             */
            if (isFirstTime())
            {
                //this means this is its first time
                MessageBox.Show(" Dear user, this is the first time you are using this app!\n We will now register you to our server so that you can use the app without any flaws. \nThanks for downloading this software!!");
                //save data to save.ini in lStore folder
                //create cache folder
                getSystemDetails(); //this get us the required details
                //Thread th = new Thread(saveXML);
                //th.Start();
                saveXML();
                //update xml to server
            }
            else 
            {
                //this means this is not the first time
                //task -> get required details
                //update and match xml from server and take required actions
            }
            //tier two task is to :
            /*
             * 1: scan in alternate thread
             * 2: match the xml details against that in server, another thread 
             * 3: check the db against last scan date/time and scan if it exceeds limit time -> upload to server
             * 
             */
              //task here is to load username,network name, files shared and rating to UI
            uname.Text = "Username: " +userName;
            nname.Text = @"Network name: \\" +localName;
            
        }
        public void saveXML()
        {
            primaryFolder = @"C:\Users\" + userName + @"\Documents\lStore";
            string xml  = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +Environment.NewLine;
            xml += "<data>" + Environment.NewLine;
            xml += "<username>" + userName + "</username>" + Environment.NewLine;
            xml += "<localname>" + localName + "</localname>" + Environment.NewLine;
            xml += "</data>";
            File.WriteAllText(primaryFolder + @"\saved.xml", xml);
        }
        /*
         * this function get details about the system from system an saves it to the variables
         */
        public void getSystemDetails()
        { 
            //get unique id
            
        }
        /*
         * this function checks if this is first time user is using this app
         * parameters: null
         * return type: bool
         */ 
        public bool isFirstTime()
        { 
            /*
             * check for the saved file if it does not exists this is first time
             */
            primaryFolder = @"C:\Users\" +userName +@"\Documents\lStore";
            if (Directory.Exists(primaryFolder))
            {
                if (File.Exists(primaryFolder + @"\saved.xml"))
                {
                    return false;
                }
                else 
                {
                    File.Create(primaryFolder + @"\saved.xml");
                }
            }
            else {
                Directory.CreateDirectory(primaryFolder);
                //now the directory is created
                System.Threading.Thread.Sleep(100);         //code shall sleep for 100 ms
                File.Create(primaryFolder + @"\saved.xml"); 
            }
            return true;
        }
        /* 
         * returns true or false corresponding to working internet connection
         */ 
        public bool isInternetConnected()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
        /*
         * this function will be called when the user clicks on the profilepic
         * it should lead to a function that allows to change the profile picture
         * need to learn about this
         * parameters: null
         * return type: void
         * thread: main thread
         */ 
       private void profilepic_Click(object sender, EventArgs e)
        {

        }
    }
}
