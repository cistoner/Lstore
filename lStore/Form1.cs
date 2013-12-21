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
using System.Collections;
using System.Web;
using System.Collections.Specialized;
//=========================================namespaces till here==============
namespace lStore
{
    public partial class lStore : Form
    {
        //=====variables here===================
        public string userName = userInfo.username, localName = userInfo.networkname;
        public string primaryFolder = @"C:\Users\" + userInfo.username + @"\Documents\lStore";
        public string ip = userInfo.ipaddress;
        public string randomFileName;   //a random file name for a file which stores temporary dat about the xml
        public ArrayList onlineUserS = new ArrayList();      //for stroring name of online user's name to be populated from db
        public ArrayList onlineUserIp = new ArrayList();    //for stroing IPs of online users
        public int onlineUsercount = 0;
        public bool isRefreshing = false;
        public float maxTime = 40000, steps = 100;
        public bool isInternet = false;
        userImage imageObj = new userImage();
        public string selectedCategory = "";    //category for search
        public int selectedSortByVal = -1;      //int val for selected option in sort by select box @ default = 0
        public bool needRefresh = false;
        public Stack<string> back = new Stack<string>();
        /** this stack will store the data for back button in listView */
        private string presentState = "";
        //==for copying a file
        StringCollection paths = new StringCollection();
        public lStore()
        {
            InitializeComponent();

            /**
             * so that usrInfo call get all the data from system
             */
            userInfo.getAllData();

            /**
             * to check if internet is connected in one of the background worker
             */ 
            isInternetConnected();   

            /** 
             * code to set the default profile image if it exists 
             */
            if (File.Exists(@"C:\Users\" + userName + @"\Documents\lStore\user.jpg"))
            {
                profilepic.Image = System.Drawing.Image.FromFile(@"C:\Users\" + userName + @"\Documents\lStore\user.jpg");
            }

            /**
             * stores the usage date and time to file
             * general stats @ :)
             */
            saveUsage();    //
            bottombar_label2.Text = "";
            pingLabel.Visible = false;
            backbutton.Visible = false;

            /**
             * code to show baloon notification
             */ 
            notifICO.BalloonTipText = "lStore active: monitering LAN activities";
            notifICO.BalloonTipTitle = "LStore: LAN sharing simplified";
            notifICO.ShowBalloonTip(1000);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            /**
             * task here is to check if this is first time or not
             * this is the first task to be performed by the tool in
             * main thread
             */
            if (isFirstTime())
            {
                /**
                 * this means this is its first time
                 */
                this.Visible = false;
                firstTime f = new firstTime();
                f.Show();
                this.Opacity = 50;
                this.Text = "Syncronising with server";
                f.BringToFront();
            }
            else 
            {

                /**
                 * 1: scan in alternate thread
                 * 2: match the xml details against that in server, another thread 
                 * 3: check the db against last scan date/time and scan if it exceeds limit time -> upload to server
                 * code now to populate list with online users 
                 * and then trigger a function to recheck online users
                 */
                uname.Text = "" + userName;
                populateUserList(users.getUsers());
                pingLabel.Visible = true;
                onlineUserRetriever.RunWorkerAsync();
            }
        }

        /**
         * background worker to retrieve online users by checking files available to them
         * and refresh list view upon completion
         */
        private void onlineUserRetriever_DoWork(object sender, DoWorkEventArgs e)
        {
            /**
             * initialisation of variables
             */ 
            string file = primaryFolder + @"\tmp\alluserlist.data";
            ;
            string fileLocation = primaryFolder + @"\tmp\searchedUsers.log";

            /**
             * emptying this file for fresh process
             */ 
            File.WriteAllText(file,""); 

            /**
             * process to get list of online users
             */ 
            onlineUser.getIpList();
            onlineUserRetriever.ReportProgress(10);
            for (int i = 1; i <= 10; i++)
            {
                System.Threading.Thread.Sleep(1000);
                onlineUserRetriever.ReportProgress(10 +i*9);
            }
                
            File.WriteAllText(primaryFolder + @"\tmp\online.data","");
            string data = File.ReadAllText(file);
            File.WriteAllText(primaryFolder + @"\tmp\online.data",data);
            /**
            string[] users = File.ReadAllLines(file);
            int i = 0;
            foreach (string user in users)
            {
                ArrayList folders = crawler.get_folders(user);
                if (folders.Count != 0)
                {
                    File.AppendAllText(tmpFile, user + Environment.NewLine);
                }
                onlineUserRetriever.ReportProgress((i * 90) / (users.Length - 1));
                i++;
            }
            */

        }
        private void onlineUserRetriever_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = (e.ProgressPercentage); 
        }
        private void onlineUserRetriever_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                bottombar_label1.Text = "Refreshing Canceled!";
            }

            else if (!(e.Error == null))
            {
                bottombar_label1.Text = "Could not refresh!";
            }
            else
            {
                bottombar_label1.Text = "User list refreshed!";
            }
            ArrayList u = users.getUsers();
            if (u.Count != 0) populateUserList(u);
            else populateUserList();
            progressBar1.Visible = false;
            progressBar1.Value = 0;
            if (needRefresh)
            {
                needRefresh = false;
                onlineUserRetriever.RunWorkerAsync();
            }
            filterOnline.RunWorkerAsync();
        }

        /**
         * background worker to filter the list created in the search
         */
        private void filterOnline_DoWork(object sender, DoWorkEventArgs e)
        {
            /**
             * make progressbar visible and show refreshing text
             */
            filterOnline.ReportProgress(0);
            
            /**
             * file base mechanisms
             */ 
            string tmpFile = primaryFolder + @"\tmp\tmp.data";
            File.WriteAllText(tmpFile,"");
            string file = primaryFolder + @"\tmp\online.data";
            string []name = File.ReadAllLines(file);

            int i = 0;
            foreach(string n in name)
            {
                ArrayList folders = crawler.get_folders(n);
                if (folders.Count != 0)
                {
                    File.AppendAllText(tmpFile, n + Environment.NewLine);
                    filterOnline.ReportProgress((i * 100)/(name.Length - 1));
                }
                i++;
            }
        }

        private void filterOnline_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                bottombar_label1.Text = "Filtering listed users";
                progressBar1.Visible = true;
            }
            progressBar1.Value = e.ProgressPercentage;
        }

        private void filterOnline_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bottombar_label1.Text = "";
            progressBar1.Visible = false;
            progressBar1.Value = 0;
            string file = primaryFolder + @"\tmp\online.data";
            string tmpFile = primaryFolder + @"\tmp\tmp.data";
            File.WriteAllText(file,File.ReadAllText(tmpFile));
            populateUserList(users.getUsers());
        }
            
        /** 
         * populateUserlist 
         */
        public void populateUserList(ArrayList user,ArrayList ips)
        {
            /** need to add code to get IP from file as well */
            onlineUserS.Clear();
            onlineUserIp.Clear();
            onlineUsers.Items.Clear();
            onlineUsercount = 0;
            /**
             * cross thread operations result in exception so you need to check weather an invoke is required prior to you using 
             * this */
            if (user != null && user.Count != 0)
            {
                foreach(string a in user)
                {
                    onlineUsercount++;
                    onlineUserS.Add(a);
                    onlineUsers.Items.Add(a);
                }
            }
            countOnline.Text = "( " + onlineUsercount + " )";
            foreach (string a in ips) { onlineUserIp.Add(a); }

        }
        /** 
         * populateUserlist 
         * accpts only one parameter that is arraylist of usernames
         */
        public void populateUserList(ArrayList user)
        {
            onlineUserS.Clear();
            onlineUsers.Items.Clear();
            onlineUsercount = 0;
            /**
             * cross thread operations result in exception so you need to check weather an invoke is required prior to you using 
             * this 
             */
            if (user != null && user.Count != 0)
            {
                foreach (string a in user)
                {
                    onlineUsercount++;
                    onlineUserS.Add(a);
                    onlineUsers.Items.Add(a);
                }
            }
            countOnline.Text = "( " + onlineUsercount + " )";
        }
        /**
         * overloaded populateUserlist 
         * this method is called when change has to be made from 
         * the existing arraylist
         */
        public void populateUserList()
        {
            onlineUsers.Items.Clear();
            onlineUsercount = 0;
            foreach (string a in onlineUserS)
            {
                onlineUsercount++;
                onlineUsers.Items.Add(a);
            }
            countOnline.Text = "( " + onlineUsercount + " )";

        }
        /** 
         * we may not need this function
         * or sync data from local db if
         * available
         */ 
        public void saveXML()
        {
            //primaryFolder = @"C:\Users\" + userName + @"\Documents\lStore";
            string xml  = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +Environment.NewLine;
            xml += "<data>" + Environment.NewLine;
            xml += "<username>" + userName + "</username>" + Environment.NewLine;
            xml += "<localname>" + localName + "</localname>" + Environment.NewLine;
            xml += "<rating>0.0</rating>" + Environment.NewLine;
            /**
             * need to resync rating,location,files_shared,hash from memmory
             * if they do not exist exit the applicatiion with an error
             * message
             */ 
            try
            {
                File.WriteAllText(primaryFolder + @"\savedfile.xml", xml);
            }
            catch (Exception ex)
            {
                saveException(ex.Message);
                MessageBox.Show("We are unable to save details to saved file! Program exits here! " +ex.Message);
                this.Close();
            }
        }
        /**
         * this function generates a random 8 digit string and returns to you
         */
        string returnRandom()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        /** this function checks for each required folders if they exist or not and create the required folder 
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
        /**
         a function to check if al required files exist and create necessory one when needed
         * params: no
         * files: saved.xml, usage.log, search: log,exceptions.log
         * dir: C:\Users\<userName>\Documents\lStore
         */
        public void repairFiles()
        {
            
            if (!File.Exists(primaryFolder + @"\savedfile.xml"))
            {
                saveXML();
            }
            if (!File.Exists(primaryFolder + @"\usage.log")){File.Create(primaryFolder + @"\usage.log");}
            if (!File.Exists(primaryFolder + @"\search.log")) { File.Create(primaryFolder + @"\search.log"); }
            if (!File.Exists(primaryFolder + @"\exceptions.log")) { File.Create(primaryFolder + @"\exceptions.log"); }
            if (!File.Exists(primaryFolder + @"\tmp\online.data")) { File.Create(primaryFolder + @"\tmp\online.data"); }
        }
        /**
         * this function checks if this is first time user is using this app
         * parameters: null
         * return type: bool
         */
        public bool isFirstTime()
        { 
            /**
             * check for the saved file if it does not exists this is first time
             */
            primaryFolder = @"C:\Users\" +userName +@"\Documents\lStore";
            if (Directory.Exists(primaryFolder))
            {
                if (File.Exists(primaryFolder + @"\savedfile.xml"))
                {
                    return false;
                }
            }
            else {
                Directory.CreateDirectory(primaryFolder);
                //now the directory is created
            }
            return true;
        }
       
       /**
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
               string source = imageObj.moveImage(fileToOpen);
               if (source != "-1")
               {
                   profilepic.Image.Dispose();
                   string destFile = @"C:\Users\" + userName + @"\Documents\lStore\user." + imageObj.getExtension(fileToOpen);
                   imageObj.GenerateThumbNail(source,destFile );
                   profilepic.Image = System.Drawing.Image.FromFile(destFile);
               }
               else MessageBox.Show("Invalid file format! file should be \"jpg\", \"jpeg\", \"png\" or \"bmp\" ");
           }
       }
        
        /** this function is responsible to initiate search */
       private void submitSearch_Click(object sender, EventArgs e)
       {
           performSearch();
       }
        /** 
         * function to perform actual search operation
         */ 
       private void performSearch()
       {
           string key = search.Text;
           if (key == "  Search here..." || key.Length == 0)
           {
               tmpLog.Text = "Enter Something first!";
               search.Focus();
           }
           else
           {
               /**
                case when something logical has been attempted
                */
               tmpLog.Text = "Searching for \" " + key + " \"";
               if (selectedCategory != "" && selectedCategory != "All")
               {
                   tmpLog.Text += " Under category \" " + selectedCategory + " \" ";
               }
               if(selectedSortByVal!=-1)
               {
                   tmpLog.Text += " Sorted by \" " + sortbySelectBox.SelectedItem.ToString() + " \" "; 
               }
               tmpLog.Text += " ....";
               //save this search to log
               writeToSearchLogs(key);

           }
       }
       /**
        * this function saves each search to a search log with date and time
        * filename: search.log
        * dir: C:\Users\<userName>\Documents\lStore
        * @param: string log: ie the search key
        */
       public void writeToSearchLogs(string log)
       {
           try
           {
               primaryFolder = @"C:\Users\" + userName + @"\Documents\lStore";
               DateTime now = DateTime.Now;
               File.AppendAllText(primaryFolder + @"\search.log", log + "||" + now + Environment.NewLine);
           }
           catch (DirectoryNotFoundException ex)
           {
               repairFolders();
               writeToSearchLogs(log);
               saveException(ex.Message);
           }
           catch (Exception ex)
           {
               throwNonRecoverableError(ex.Message);
           }
       }
       /**
        * this function saves each time this application is opened to a log file 
        * filename: usage.log
        * dir: C:\Users\<userName>\Documents\lStore
        * no params
        */
       public void saveUsage()
       {
           try
           {
               primaryFolder = @"C:\Users\" + userName + @"\Documents\lStore";
               DateTime now = DateTime.Now;
               File.AppendAllText(primaryFolder + @"\usage.log", now + Environment.NewLine);
           }
           catch (DirectoryNotFoundException ex)
           {
               repairFolders();
               saveUsage();         //exception handeling
               saveException(ex.Message);
           }
           catch (Exception ex)
           {
               throwNonRecoverableError(ex.Message);
           }
       }
       /**
        * a function to save exception to a log file!!
        * params: string ex: the exception message
        * filename: exceptions.log
        * dir: C:\Users\<userName>\Documents\lStore
        */
       public void saveException(string ex)
       {
           try
           {
               primaryFolder = @"C:\Users\" + userName + @"\Documents\lStore";
               DateTime now = DateTime.Now;
               File.AppendAllText(primaryFolder + @"\exceptions.log", ex + "||" + now + Environment.NewLine);
           }
           catch (DirectoryNotFoundException exep)
           {
               repairFolders();
               saveException(ex);
               saveException(exep.Message);
           }
           catch (Exception exep)
           {
               MessageBox.Show("Some error occured: " + exep.Message);
           }
       }
        /**
         * a function to handle non defined exceptions to be added later into account
         * save the exception into log and display the messageBox
         * @param: string err: ie the error message
         */
       public void throwNonRecoverableError(string err)
       {
           saveException(err);
           MessageBox.Show("Some error occured: " +err);
       } 
       private void search_TextChanged(object sender, EventArgs e)
       {
           /**
            codes to be implemented here to make a UI like google ajax call 
            * search suggestions
            */
       }
       private void filterUser_Click(object sender, EventArgs e)
       {
           filterUser.Focus();
       }
        /**
         * when ever user write some data to user filter this code refreshes the list
         */ 
       private void filterUser_TextChanged(object sender, EventArgs e)
       {
           onlineUsers.Items.Clear();
           string searchkey = filterUser.Text.ToLower();
           if (searchkey.Length == 0 || searchkey == "search....") 
           {
               foreach (string name in onlineUserS)
               {
                   onlineUsers.Items.Add(name);
               }
           }
           
           foreach(string name in onlineUserS)
           {
               if (name.ToLower().IndexOf(searchkey) != -1)
               {
                   onlineUsers.Items.Add(name);
               }
           }
       }
        /** 
         * a function to initiate the internet check background worker
         */ 
       public void isInternetConnected()
       {
           //internetState.Text = "Checking Internet";
           //internetState.ForeColor = System.Drawing.Color.Blue;
           string processing = primaryFolder + @"\offline.png";
           internetstateImg.BackgroundImage = System.Drawing.Image.FromFile(processing);
           bgw_internetstate.RunWorkerAsync();
       }
        /** 
         * a background worker to check state of connected internet
         * it checks by opeing url: google.co.in
         * and checks the received error state
         */ 
       private void bgw_internetstate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
       {
           HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://www.google.co.in/");
           webRequest.AllowAutoRedirect = false;
           try
           {
               HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
               isInternet = true;
           }
           catch (WebException ex) { isInternet =  false; }
       }
        /**
         * this event occours when the internet check bgw finish operation
         */ 
       private void bgw_internetstate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
       {
           string online = primaryFolder +@"\online.png";
           string offline = primaryFolder + @"\offline.png";
           if (isInternet)
           { 
               internetstateImg.BackgroundImage = System.Drawing.Image.FromFile(online);
               notifICO.BalloonTipText = "You are online. Stay online for better performance!";
               notifICO.BalloonTipTitle = "LStore: LAN sharing simplified";
               
           }
           else 
           {
               internetstateImg.BackgroundImage = System.Drawing.Image.FromFile(offline);
               notifICO.BalloonTipText = "You are offline. Get online for better performance!";
               notifICO.BalloonTipTitle = "LStore: LAN sharing simplified";
           }
           notifICO.ShowBalloonTip(1000);
       }

        /**
         * this event runs when someone clicks on online user list
         * need to add code to get file size and rating and
         * CATEGORY
         */
       private void clearStack(Stack<string> st)
       {
           st.Clear();
       }
       private void onlineUsers_SelectedIndexChanged(object sender, EventArgs e)
       {
           string user;
           try
           {
               user = onlineUsers.SelectedItem.ToString();
               crawler.root = @"\\" + user + @"\";
           }
           catch(Exception ex)
           {
               saveException(ex.Message);
               return;
           }
           //clearStack(back);
           //back.Push(@"\\" +user);
           ArrayList folders = crawler.get_folders(user);
           if (folders.Count == 0 || folders[0].ToString() == "-1" )
           {
               onlineUserS.Remove(user);
               populateUserList();  //calling for refreshing the user UI
               bottombar_label2.Text = "User is not available or not accessible. User removed from list";
               return;
           }
           workspace.Items.Clear();
           for (int i = 0; i < folders.Count; i++)
           {
               ListViewItem foo = new ListViewItem(new string[] { folders[i].ToString(), crawler.getOwner(folders[i].ToString()), "", crawler.getCategory(folders[i].ToString()), "" });
               workspace.Items.Add(foo);
               
           }

       }

        /**
         * event when mouse dbl click occurs in listviw detail item 
         */
       private void workspace_MouseDoubleClick(object sender, MouseEventArgs e)
       {
           string sel;
           try
           {
               sel = workspace.SelectedItems[0].Text;
           }
           catch (Exception ex) { return; }
           if (sel != "..")
           {
               if (presentState.Length != 0) back.Push(presentState);
               presentState = sel;
               refreshListView(sel);
           }
           else
           {
               try
               {
                   refreshListView(back.Pop());
               }
               catch (Exception ex) 
               {
                   saveException(ex.Message);
               }
           }
       }

       /**
        * when enter button is pressed
        * in listview
        * overan item
        */ 
       private void workspace_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
           {
               string sel;
               try
               {
                   sel = workspace.SelectedItems[0].Text;
               }
               catch (Exception ex) 
               {
                   MessageBox.Show(ex.Message);
                   return;
               }
               if (sel != "..")
               {
                   if (presentState.Length != 0) back.Push(presentState);
                   presentState = sel;
                   refreshListView(sel);
               }
               else
               {
                   try
                   {
                       refreshListView(back.Pop());
                   }
                   catch (Exception ex)
                   {
                       //
                       MessageBox.Show(ex.Message);
                       saveException(ex.Message);
                   }
               }
           }
       }

       /** 
        * this function deals with refresing the listview UI when even user 
        * clicks or enter clikc
        * on the listView item
        * @param: directory to access [ string : "sel" ]
        */ 
       private void refreshListView(string sel)
       {
           
           workspace.Items.Clear();
           ListViewItem foo = new ListViewItem(new string[] { "..", "", "", "UP", "" });
           workspace.Items.Add(foo);
           try
           {
               string[] folders;
               try
               {
                   folders = Directory.GetDirectories(sel);
               }
               catch (UnauthorizedAccessException ex)
               {
                   MessageBox.Show(@"Oops! you do not seem to have access to this folder. Our bad :/");
                   return;
               }
               int len = folders.Length;
               for (int i = 0; i < len; i++)
               {
                   try
                   {
                       workspace.Items.Add(new ListViewItem(new string[] { folders[i], crawler.getOwner(folders[i]), "", "folder", "--NA--" }));
                        
                   }
                   catch (Exception ex) 
                   {
                       MessageBox.Show(ex.Message); 
                       continue;
                   }
               }
               string[] files = Directory.GetFiles(sel);
               len = files.Length;
               for (int i = 0; i < len; i++)
               {
                   FileInfo f = new FileInfo(files[i]);
                   long s = f.Length;
                   string size;
                   if (s / (1024 * 1024) <= 1) { 
                        if(s / 1024 <= 1)
                        {
                            size = s.ToString() +"bytes";
                        }
                        else size = (s/1024).ToString() + "kb";
                   }
                   else size = (s/(1024*1024)).ToString() +"mb";
                   try
                   {
                       workspace.Items.Add(new ListViewItem(new string[] { files[i], crawler.getOwner(files[i]), size, crawler.getCategory(files[i]), "--NA--" }));
                   }
                   catch (Exception ex) 
                   { 
                       //
                       MessageBox.Show(ex.Message); 
                       continue;
                   }
               }
           }
           catch (ArgumentException ex)
           {
               sel = sel.Replace(@"\\", "");
               clearStack(back);
               back.Push(sel);
               ArrayList folders = crawler.get_folders(sel);
               workspace.Items.Clear();
               for (int i = 0; i < folders.Count; i++)
               {
                   workspace.Items.Add(new ListViewItem(new string[] { folders[i].ToString(), crawler.getOwner(folders[i].ToString()), "--NA--", "--NA--", "--NA--" }));
               }
           }
           catch (IOException ex)
           {
               //this means it can be a file
               try
               {
                   openDirec(sel);
               }
               catch (Exception e)
               { 
               
               }
               refreshListView(back.Pop());
           }
       }

        /** 
         * invoked when select for categories is changed
         */ 
       private void selectCategories_SelectedIndexChanged(object sender, EventArgs e)
       {
           selectedCategory = selectCategories.SelectedItem.ToString();
           /**
            * can add code to re-search on category change
            */
           performSearch();
       }

        /** 
         * this function select complete text of the
         * input box search
         * when clicked
         */ 
       private void search_Click(object sender, EventArgs e)
       {
           search.SelectAll();
       }

        /**
         * listner to check enter key was pressed while typing in 
         * search nput
         * and call the search function when the event is triggered
         */ 
       private void search_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
           {
               performSearch();
           }
       }
        /** 
         * triggered when the 
         * sort by select box is changed
         */ 
       private void sortbySelectBox_SelectedIndexChanged(object sender, EventArgs e)
       {
           selectedSortByVal = sortbySelectBox.SelectedIndex;
           performSearch();
       }

        //==============menu==actions
        /**
         * exit button in FILE menu
         */ 
       private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
       {
           this.Close();
       }

        /** 
         * exit button in notification bar
         * at bottom of menu
         */ 
       private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
       {
           this.Close();
       }

        /** 
         * option: social
         * @suboption: Chat
         */ 
       private void cHATToolStripMenuItem_Click(object sender, EventArgs e)
       {
           chat c = new chat();
           c.Show();
       }
        /** 
         * function called when form exits
         */ 
       private void lStore_FormClosed(object sender, FormClosedEventArgs e)
       {
           notifICO.BalloonTipText = "You can access lStore from Notification panel anytime. Stay connected!";
           notifICO.ShowBalloonTip(1000);
       }
       private void lStore_FormClosing(object sender, FormClosingEventArgs e)
       {
           /** 
            * a dialog box for confirmation if you want to exit
            */
       }
       private void filterUser_MouseClick(object sender, MouseEventArgs e)
       {
           if (filterUser.Text == "search....") filterUser.SelectAll();
       }
        /**
         * function to open a folder in explorer or a file
         * in default viewer when usr clicks on the option 
         * by right clicking on that field
         */
       private void openDirec(string sel)
       {
           try
           {
               System.Diagnostics.Process.Start(@sel);
           }
           catch (Exception ex)
           {
               MessageBox.Show("Unable to open, Exception: " + ex.Message);
           }
       }
       private void lv_menu_open_Click(object sender, EventArgs e)
       {
           string sel = workspace.SelectedItems[0].Text;
           openDirec(sel);
       }
        /** 
         * function to copy a file to clipboard when user clicks on that option
         */ 
       private void toMemoryToolStripMenuItem_Click(object sender, EventArgs e)
       {
           paths.Clear();
           try
           {
               paths.Add(workspace.SelectedItems[0].Text);
               Clipboard.SetFileDropList(paths);
           }
           catch (Exception ex)
           {
               saveException(ex.Message);
               return;
           }
       }

       /**
        * function called when user click on About Me link
        * open the aboutUser form
        */ 
       private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           aboutUser obj = new aboutUser();
           obj.Show();
       }

       

      
       

   }
}
