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
        public string ip, baseaddr;
        public lStore()
        {
            InitializeComponent();
            localName = System.Environment.MachineName;
            if (!isInternetConnected()) { internetState.Text = "No internet connection"; }
            else { internetState.Text = "Connected to internet"; }
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
            //need of this function need to be thought off again
        }
        /* this function checks for each required folders if they exist or not and create the required folder 
         * according to need
         * params:no
         * dir: C:\Users\<userName>\Documents\lStore
         * folders: ...lStore\, ...lStore\tmp
         */
        public void repairFolders()
        {
            string mainFolder = @"C:\Users\" + userName + @"\Documents\lStore";
            if (!Directory.Exists(mainFolder)) { Directory.CreateDirectory(mainFolder); }
            string tmpFolder = mainFolder + @"\tmp";
            if (!Directory.Exists(tmpFolder)) { Directory.CreateDirectory(tmpFolder); }
        }
        /*
         a function to check if al required files exist and create necessory one when needed
         * params: no
         * files: saved.xml, usage.log, search: log
         * dir: C:\Users\<userName>\Documents\lStore
         */
        public void repairFiles()
        {
            primaryFolder = @"C:\Users\" + userName + @"\Documents\lStore";
            if (!File.Exists(primaryFolder + @"\saved.xml"))
            {
                saveXML();
            }
            if (!File.Exists(primaryFolder + @"\usage.log")){File.Create(primaryFolder + @"\usage.log");}
            if (!File.Exists(primaryFolder + @"\search.log")){File.Create(primaryFolder + @"\search.log");}
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
        * program to change profile image
        * this function will be called when the user clicks on the profilepic
        * it should lead to a function that allows to change the profile picture
        * need to learn about this
        * parameters: null
        * return type: void
        * thread: main thread
        */
       private void linkChangeImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           var FD = new System.Windows.Forms.OpenFileDialog();
           if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
           {
               string fileToOpen = FD.FileName;
               string[] splitname = fileToOpen.Split('.');
               string extension = splitname[(splitname.Length - 1)].ToLower();
               string profileImageDirec = @"C:\Users\" + userName + @"\Documents\lStore\tmp\user." + extension;
               if (extension == "jpg" || extension == "png" || extension == "bmp" || extension == "jpeg")
               {
                   //System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);
                   //System.IO.StreamReader reader = new System.IO.StreamReader(fileToOpen);
                   if (File.Exists(profileImageDirec)) File.Delete(profileImageDirec);
                   try
                   {
                       File.Copy(fileToOpen, profileImageDirec);
                   }
                   catch (DirectoryNotFoundException ex)
                   {
                       repairFolders();
                       File.Copy(fileToOpen, profileImageDirec);
                   }
                   catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message); 
                        //more actions need to be added to counter a situation like this
                   }
                   //====task is to resize image in  tmp folder now=====
                   /*
                    * process: resize img in tmp -> delete user.xt in lstore@/ and 
                    * move lstore/tmp/user.xt to lstore/user.xt
                    * task done
                    * a loader till this task is finished
                    */ 
                   //after this reload the image pane inmain UI
               }
               else
               {
                   MessageBox.Show("Invalid file format! file should be \"jpg\", \"jpeg\", \"png\" or \"bmp\" ");
               }
           }
       }
        /* this function is responsible to initiate search */
       private void submitSearch_Click(object sender, EventArgs e)
       {
           string key = search.Text;
           if (key == "  Search here..." || key.Length == 0)
           {
               tmpLog.Text = "Enter Something first!";
               search.Focus();
           }
           else
           {    
               /*
                case when something logical has been attempted
                */
               tmpLog.Text = "Searching for \" " + key + " \"...";
               //save this search to log
               writeToSearchLogs(key);
               
           }
       }
       /*
        * this function saves each search to a search log with date and time
        * filename: search.log
        * dir: C:\Users\<userName>\Documents\lStore
        * @param: string log: ie the search key
        */
       public void writeToSearchLogs(string log)
       {
           primaryFolder = @"C:\Users\" + userName + @"\Documents\lStore";
           DateTime now = DateTime.Now;
           File.AppendAllText(primaryFolder +@"\search.log", log +"||" +now + Environment.NewLine);
       }
       /*
        * this function saves each time this application is opened to a log file 
        * filename: usage.log
        * dir: C:\Users\<userName>\Documents\lStore
        * no params
        */
       public void saveUsage()
       {
           primaryFolder = @"C:\Users\" + userName + @"\Documents\lStore";
           DateTime now = DateTime.Now;
           File.AppendAllText(primaryFolder + @"\usage.log", now + Environment.NewLine);
       }
    }
}
