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
            }
            else 
            {
                //this means this is not the first time
                //task -> get required details
                getSystemDetails(); //this get us the required details
                //try to do ne
            }
            
        }
        public void saveXML()
        {
            primaryFolder = @"C:\Users\" + userName + @"\Documents\lStore";
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<data>";
            xml += "<username>" +userName +"</username>";
            xml += "<localname>" +localName +"</localname>";
            xml += "</data";
            File.WriteAllText(primaryFolder + @"\saved.xml",xml);
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
                    return true;
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
            return false;
        }
        /* 
         * returns true or false corresponding to working internet connection
         */ 
        public bool isInternetConnected()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
