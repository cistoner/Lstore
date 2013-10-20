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
        public ArrayList onlineUser = new ArrayList();      //for stroring name of online user's name to be populated from db
        public ArrayList onlineUserIp = new ArrayList();    //for stroing IPs of online users
        delegate void myDelegate(ArrayList u, ArrayList i);
        public int onlineUsercount = 0;
        public bool isRefreshing = false;
        public float maxTime = 40000, steps = 100;
        public bool isInternet = false;
        userImage imageObj = new userImage();
        public string selectedCategory = "";    //category for search
        public int selectedSortByVal = -1;      //int val for selected option in sort by select box @ default = 0
        public bool needRefresh = false;
        public lStore()
        {
            InitializeComponent();
            /*
             * so that usrInfo call get all the data from system
             */
            userInfo.getAllData();
            /*
             * to check if internet is connected in one of the background worker
             */ 
            isInternetConnected();       
            /* 
             * code to set the default profile image if it exists 
             */
            if (File.Exists(@"C:\Users\" + userName + @"\Documents\lStore\user.jpg"))
            {
                profilepic.Image = System.Drawing.Image.FromFile(@"C:\Users\" + userName + @"\Documents\lStore\user.jpg");
            }
            /*
             * stores the usage date and time to file
             * general stats @ :)
             */
            saveUsage();    //
            bottombar_label2.Text = "";
            pingLabel.Visible = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            /*
             * task here is to check if this is first time or not
             * this is the first task to be performed by the tool in
             * main thread
             */
            if (isFirstTime())
            {
                /*
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
                //tier two task is to :
                /*
                 * 1: scan in alternate thread
                 * 2: match the xml details against that in server, another thread 
                 * 3: check the db against last scan date/time and scan if it exceeds limit time -> upload to server
                 * 
                 */
                rating.Text = " " + userInfo.rating;
                codeLocation.Text = " " + userInfo.location;
                countFilesShared.Text = " " + userInfo.files_shared;
                uname.Text = "" + userName;
                nname.Text = @"\\" + localName;
                
                /*
                 * code now to populate list with online users 
                 * and then trigger a function to recheck online users
                 */
                populateUserList(users.getUsers());
                bg1.RunWorkerAsync();
                pingLabel.Visible = true;
                onlineUserRetriever.RunWorkerAsync();
            }
        }
        /*
         * background worker to retrieve online users by checking files available to them
         * and refresh list view upon completion
         */
        private void onlineUserRetriever_DoWork(object sender, DoWorkEventArgs e)
        {
            string file = primaryFolder + @"\tmp\alluserlist.data";
            string tmpFile = primaryFolder + @"\tmp\tmp.data";
            File.WriteAllText(tmpFile,"");
            if (!File.Exists(file))
            {
                //need to fetch this from server
            }
            else
            {
                string dat = File.ReadAllText(file);
                string[] arr = dat.Split('*');
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    ArrayList folders = crawler.get_folders(arr[i]);
                    if (folders.Count != 0)
                    { 
                        // this means the user is available
                        File.AppendAllText(tmpFile,arr[i] + Environment.NewLine);
                        
                    }
                    //report progress here
                    onlineUserRetriever.ReportProgress(i / (arr.Length-1) * 100);
                }
            }

        }
        private void onlineUserRetriever_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = (e.ProgressPercentage);
            File.AppendAllText(primaryFolder + @"\tmp\test.data", e.ProgressPercentage +Environment.NewLine);
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
            string data = File.ReadAllText(primaryFolder +@"\tmp\tmp.data");
            File.WriteAllText(primaryFolder + @"\tmp\online.data",data);
            ArrayList u = users.getUsers();
            if (u.Count != 0) populateUserList(u);
            else populateUserList();
            progressBar1.Visible = false;
            if (needRefresh)
            {
                needRefresh = false;
                onlineUserRetriever.RunWorkerAsync();
            }
        }
        /*
         * background worker to generate list of online user by IP method and 
         * and upon completion match it with local db and 
         * send new device name to server
         */ 
        private void bg1_DoWork(object sender, DoWorkEventArgs e)
        {
            isRefreshing = true;
            File.WriteAllText(primaryFolder + @"\tmp\tmp_.data", "");
            for (int i = 0; i <= 255; i++)
            {
                string ip = (string)userInfo.baseaddress + "." + i.ToString();
                Ping p = new Ping();
                p.PingCompleted += new PingCompletedEventHandler(p_PingCompleted);
                try
                {
                    p.SendAsync(ip, 100, ip);
                    File.AppendAllText(primaryFolder + @"\tmp\test_.data","ping sent to " +ip + Environment.NewLine);
                }
                catch (PingException ex)
                {
                    File.AppendAllText(primaryFolder + @"\tmp\test_.data", "could not ping " + ip +" || " +ex.Message + Environment.NewLine);
                    continue;
                }
            }
        }
        /*
         * a method to recieve the ping()
         */
        public void p_PingCompleted(object sender, PingCompletedEventArgs e)
        {

            string ip = (string)e.UserState;
            if (e.Reply != null && e.Reply.Status == IPStatus.Success)
            {
                string name;
                try
                {
                    IPHostEntry hostEntry = Dns.GetHostEntry(ip);
                    try
                    {
                        name = hostEntry.HostName;
                        File.AppendAllText(primaryFolder + @"\tmp\tmp_.data", name + Environment.NewLine);
                    }
                    catch (SocketException ex) { }
                }
                catch (Exception ex) { }
            }
            try
            {
                bg1.ReportProgress(0);
            }
            catch (Exception ex) { }
        }
        private void bg1_ProgressChanged_1(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //can add a progress bar at bottom label later
        }
        private void bg1_RunWorkerCompleted_1(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            isRefreshing = false;
            pingLabel.Visible = false;
            string data = File.ReadAllText(primaryFolder + @"\tmp\alluserlist.data");
            string []newNname = File.ReadAllLines(primaryFolder + @"\tmp\tmp_.data");
            bool flag = false;
            int len = newNname.Length,i;
            for (i = 0; i < len; i++)
            {
                if (data.IndexOf(newNname[i]) == -1)
                {
                    flag = true;
                    File.AppendAllText(primaryFolder + @"\tmp\alluserlist.data",newNname[i] +"*");
                }
            }
            if (flag) 
            {

                if (onlineUserRetriever.IsBusy)
                {
                    needRefresh = true;
                }
                else
                {
                    onlineUserRetriever.RunWorkerAsync();
                }
            }
            /* 
             * task: match the list so generated with users in db
             * and whichsoever does not exists
             * make a list and post it to server
             */

        }      
        /* 
         * populateUserlist 
         */
        public void populateUserList(ArrayList user,ArrayList ips)
        {
            /* need to add code to get IP from file as well */
            onlineUser.Clear();
            onlineUserIp.Clear();
            onlineUsers.Items.Clear();
            onlineUsercount = 0;
            /*
             * cross thread operations result in exception so you need to check weather an invoke is required prior to you using 
             * this */
            if (user != null && user.Count != 0)
            {
                foreach(string a in user)
                {
                    onlineUsercount++;
                    onlineUser.Add(a);
                    onlineUsers.Items.Add(a);
                }
            }
            countOnline.Text = "( " + onlineUsercount + " )";
            foreach (string a in ips) { onlineUserIp.Add(a); }

        }
        /* 
         * populateUserlist 
         * accpts only one parameter that is arraylist of usernames
         */
        public void populateUserList(ArrayList user)
        {
            onlineUser.Clear();
            onlineUsers.Items.Clear();
            onlineUsercount = 0;
            /*
             * cross thread operations result in exception so you need to check weather an invoke is required prior to you using 
             * this 
             */
            if (user != null && user.Count != 0)
            {
                foreach (string a in user)
                {
                    onlineUsercount++;
                    onlineUser.Add(a);
                    onlineUsers.Items.Add(a);
                }
            }
            countOnline.Text = "( " + onlineUsercount + " )";
        }
        /*
         * overloaded populateUserlist 
         * this method is called when change has to be made from 
         * the existing arraylist
         */
        public void populateUserList()
        {
            onlineUsers.Items.Clear();
            onlineUsercount = 0;
            foreach (string a in onlineUser)
            {
                onlineUsercount++;
                onlineUsers.Items.Add(a);
            }
            countOnline.Text = "( " + onlineUsercount + " )";

        }
        /* 
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
            /*
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
        /*
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
        
        /* this function is responsible to initiate search */
       private void submitSearch_Click(object sender, EventArgs e)
       {
           performSearch();
       }
        /* 
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
               /*
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
       /*
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
       /*
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
       /*
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
        /*
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
           /*
            codes to be implemented here to make a UI like google ajax call 
            * search suggestions
            */
       }
       private void filterUser_Click(object sender, EventArgs e)
       {
           filterUser.Focus();
       }
        /*
         * when ever user write some data to user filter this code refreshes the list
         */ 
       private void filterUser_TextChanged(object sender, EventArgs e)
       {
           onlineUsers.Items.Clear();
           string searchkey = filterUser.Text.ToLower();
           if (searchkey.Length == 0 || searchkey == "search....") 
           {
               foreach (string name in onlineUser)
               {
                   onlineUsers.Items.Add(name);
               }
           }
           
           foreach(string name in onlineUser)
           {
               if (name.ToLower().IndexOf(searchkey) != -1)
               {
                   onlineUsers.Items.Add(name);
               }
           }
       }
        /* 
         * a function to initiate the internet check background worker
         */ 
       public void isInternetConnected()
       {
           internetState.Text = "Checking Internet";
           internetState.ForeColor = System.Drawing.Color.Blue;
           bgw_internetstate.RunWorkerAsync();
       }
        /* 
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
        /*
         * this event occours when the internet check bgw finish operation
         */ 
       private void bgw_internetstate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
       {
           if (isInternet)
           { 
               internetState.Text = "Connected to internet";
               internetState.ForeColor = System.Drawing.Color.Green;
           }
           else 
           {
               internetState.Text = "No internet connection";
               internetState.ForeColor = System.Drawing.Color.Red;
           }
       }
        /*
         * this event runs when someone clicks on online user list
         * need to add code to get file size and rating and
         * CATEGORY
         */ 
       private void onlineUsers_SelectedIndexChanged(object sender, EventArgs e)
       {
           string user;
           try
           {
               user = onlineUsers.SelectedItem.ToString();
           }
           catch(Exception ex)
           {
               saveException(ex.Message);
               return;
           }
           ArrayList folders = crawler.get_folders(user);
           if (folders.Count == 0 || folders[0].ToString() == "-1" )
           {
               onlineUser.Remove(user);
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
        /*
         * event when mouse dbl click occurs in listviw detail item 
         */
       private void workspace_MouseDoubleClick(object sender, MouseEventArgs e)
       {
           refreshListView();          
       }
       /*
        * when enter button is pressed
        * in listview
        * overan item
        */ 
       private void workspace_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
           {
               refreshListView();
           }
       }
       /* 
        * this function deals with refresing the listview UI when even user 
        * clicks or enter clikc
        * on the listView item
        */ 
       private void refreshListView()
       {
           string sel;
           try
           {
               sel = workspace.SelectedItems[0].Text;
           }
           catch (Exception ex) { return; }
           workspace.Items.Clear();
           ListViewItem foo = new ListViewItem(new string[] { crawler.getUpUrl(sel), crawler.getOwner(crawler.getUpUrl(sel)), "", "UP", "" });
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
                   catch (Exception ex) { continue; }
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
                   catch (Exception ex) { continue; }
               }
           }
           catch (ArgumentException ex)
           {
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

           }
       }
        /* 
         * invoked when select for categories is changed
         */ 
       private void selectCategories_SelectedIndexChanged(object sender, EventArgs e)
       {
           selectedCategory = selectCategories.SelectedItem.ToString();
           /*
            * can add code to re-search on category change
            */
           performSearch();
       }
        /* 
         * this function select complete text of the
         * input box search
         * when clicked
         */ 
       private void search_Click(object sender, EventArgs e)
       {
           search.SelectAll();
       }
        /*
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
        /* 
         * triggered when the 
         * sort by select box is changed
         */ 
       private void sortbySelectBox_SelectedIndexChanged(object sender, EventArgs e)
       {
           selectedSortByVal = sortbySelectBox.SelectedIndex;
           performSearch();
       }
        //==============menu==actions
        /*
         * exit button in FILE menu
         */ 
       private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
       {
           this.Close();
       }
        /* 
         * exit button in notification bar
         * at bottom of menu
         */ 
       private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
       {
           this.Close();
       }
        /* 
         * option: social
         * @suboption: Chat
         */ 
       private void cHATToolStripMenuItem_Click(object sender, EventArgs e)
       {
           chat c = new chat();
           c.Show();
       }
        /* 
         * function called when form exits
         */ 
       private void lStore_FormClosed(object sender, FormClosedEventArgs e)
       {

       }

       
       private void filterUser_MouseClick(object sender, MouseEventArgs e)
       {
           if (filterUser.Text == "search....") filterUser.SelectAll();
       }

       

   }
}
