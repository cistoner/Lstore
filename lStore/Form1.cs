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
using System.Windows;
using System.Data.SqlClient;

//=========================================namespaces till here==============
namespace lStore
{
    public partial class lStore : Form
    {
        //=====variables here===================
        public string userName = userInfo.username, localName = userInfo.networkname;
        public string primaryFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\lStore";
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

        /**
         * for preferences
         */
        preferences prefObj = new preferences();

        /**
         * for tootip
         */
        public ToolTip tT { get; set; }

        /**
         * for viewing folders
         */
        public string currentSelection = string.Empty;
        private ArrayList currentSelectionfolders = new ArrayList();

        /** 
         * this stack will store the data for back button in listView 
         */
        public Stack<string> back = new Stack<string>();
        

        //==for copying a file
        StringCollection paths = new StringCollection();
        public lStore()
        {
            InitializeComponent();
            tT = new ToolTip();

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
            //backbutton.Visible = false;
            //presentLocation.Visible = false;           

            /*
            TitleBarButtons titleBar = new TitleBarButtons();
            titleBar.location = new Point(this.Location.X,this.Location.Y);
            titleBar.Show();
            titleBar.Owner = this;
             */ 

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
            bool done = false;

            /**
             * initialisation of variables
             */ 
            string file = primaryFolder + @"\tmp\alluserlist.data";
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
                System.Threading.Thread.Sleep(2000);
                onlineUserRetriever.ReportProgress(10 +i*9);
            }
            
            /**
             * to avoid exception of different process using same file
             * we attempt to a task till it is doen
             * by using a var @bool done
             */
            while (!done)
            {
                try
                {
                    File.WriteAllText(primaryFolder + @"\tmp\online.data", "");
                    done = true;
                }
                catch (Exception ex)
                {
                    System.Threading.Thread.Sleep(100);    
                }
            }

            string data = string.Empty;
            done = false;
            while (!done)
            {
                try
                {
                    data = File.ReadAllText(file);
                    done = true;
                }
                catch (Exception ex)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }

            done = false;
            while (!done)
            {
                try
                {
                    File.WriteAllText(primaryFolder + @"\tmp\online.data", data);
                    done = true;
                }
                catch (Exception ex)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
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
                refreshbutton1.Visible = true;
            }

            else if (!(e.Error == null))
            {
                bottombar_label1.Text = "Could not refresh!";
                refreshbutton1.Visible = true;
            }
            else
            {
                bottombar_label1.Text = "User list refreshed!";
            }

            /**
             * this means if this is the first time form load 
             * then app should show temporary user list who replies to ping
             */
            ArrayList u = users.getUsers();
            if (u.Count != 0) populateUserList(u);
            else populateUserList();
            progressBar1.Visible = false;
            progressBar1.Value = 0;

            /**
            if (needRefresh)
            {
                needRefresh = false;
                onlineUserRetriever.RunWorkerAsync();
            }
             * */
            bottombar_label1.Text = "Filtering listed users";
            progressBar1.Visible = true;
            filterOnline.RunWorkerAsync();
        }

        /**
         * background worker to filter the list created in the search
         */
        private void filterOnline_DoWork(object sender, DoWorkEventArgs e)
        {
            
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
                    try
                    {
                        filterOnline.ReportProgress((i * 100) / (name.Length));
                    }
                    catch(Exception ex){}
                }
                i++;
            }
        }

        private void filterOnline_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
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
            refreshbutton1.Visible = true;
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
            if (!Directory.Exists(primaryFolder)) { Directory.CreateDirectory(primaryFolder); }
            string tmpFolder = primaryFolder + @"\tmp";
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
                   string destFile = primaryFolder +@"\user." + imageObj.getExtension(fileToOpen);
                   imageObj.GenerateThumbNail(source,destFile );
                   profilepic.Image = System.Drawing.Image.FromFile(destFile);
               }
               else MessageBox.Show("Invalid file format! file should be \"jpg\", \"jpeg\", \"png\" or \"bmp\" ");
           }
       }
        
        /**
         * array list to hold result of a database based search
         */
        private ArrayList searchresult = new ArrayList();
        private ArrayList searchresultRating = new ArrayList();
        private ArrayList searchresultDisplayed = new ArrayList();

        /**
         * variable to hold the search result
         */ 
        private string searchkey = string.Empty;
        private int searchLimit = 100;
        private int searchPage = 1;

        /** 
         * this function is responsible to initiate search 
         */
        private void submitSearch_Click(object sender, EventArgs e)
        {
            performSearch();
        }

        /** 
         * function to perform actual search operation
         */ 
       private void performSearch()
       {
           searchprogressbar.Value = 0;
           searchprogressbar.Visible = true;
           loader.Visible = true;
           string key = search.Text;
           if (key == "  Search here..." || key.Length == 0)
           {
               tmpLog.Text = "Enter Something first!";
               search.Focus();
               return;
           }
           else
           {
               /**
                case when something logical has been attempted
                */
               presentLocation.Text = @"\SEARCH:" +key;
               tmpLog.Text = "Searching for \" " + key + " \"";
               searchkey = key;
               
               //if (selectedCategory != "" && selectedCategory != "All")
               //{
               //    tmpLog.Text += " Under category \" " + selectedCategory + " \" ";
               //}
               //if(selectedSortByVal!=-1)
               //{
               //    tmpLog.Text += " Sorted by \" " + sortbySelectBox.SelectedItem.ToString() + " \" "; 
               //}
               //tmpLog.Text += " ....";
               //save this search to log
               writeToSearchLogs(key);
               searchbgw.RunWorkerAsync();
           }
       }

        /**
         * background worker to do searching operation
         */
       private void searchbgw_DoWork(object sender, DoWorkEventArgs e)
       {
           if (searchkey.Length != 0)
           { 
                /**
                 * this means we gotta perform search
                 * giving 20% to sql connection and search 
                 */
               searchbgw.ReportProgress(10);
               searchresult.Clear();
               searchresultDisplayed.Clear();
               searchresultRating.Clear();

               //db operation from now
               searchbgw.ReportProgress(20);
               SqlConnection mycon = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\q4sp9x.mdf;Integrated Security=True;Connect Timeout=30");
               var command = mycon.CreateCommand();
               string sqlc = "SELECT filename,rating FROM dbo.files WHERE keywords LIKE @param";
               SqlCommand comm = new SqlCommand(sqlc, mycon);
               comm.Parameters.Add(new SqlParameter("param", "%" +searchkey +"%"));
               mycon.Open();
               SqlDataReader r = comm.ExecuteReader();
               searchbgw.ReportProgress(50);
               if (r.HasRows)
               {
                   while (r.Read())
                   {
                       searchresult.Add(r["filename"].ToString());
                       searchresultRating.Add(r["rating"].ToString());
                       searchresultDisplayed.Add("f");
                   }
                   searchbgw.ReportProgress(100);
               }
               else
               {
                   /**
                    * search complete no data found
                    */ 
                   searchbgw.ReportProgress(100);
               } 
               mycon.Close();
           
           }
       }

       private void searchbgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
       {
           searchprogressbar.Value = e.ProgressPercentage;
       }

       private void searchbgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
       {
           workspace.Items.Clear();
           foreach (string result in searchresult)
           {
               string tmp = result.Replace("/",@"\");
               if (Directory.Exists(tmp))
               {
                   int id = searchresult.IndexOf(result);
                   workspace.Items.Add(new ListViewItem(new string[] { tmp, crawler.getOwner(tmp), "", "Available", searchresultRating[id].ToString() }));
                   searchresultDisplayed[id] = "t";
               }
           }
           foreach (string result in searchresult)
           {
               string tmp = result.Replace("/", @"\");
               int id = searchresult.IndexOf(result);
               if (searchresultDisplayed[id].ToString() == "f")
               {
                   workspace.Items.Add(new ListViewItem(new string[] { tmp, crawler.getOwner(tmp), "", "Not Available", searchresultRating[id].ToString() }));
               }
           }
           searchprogressbar.Visible = false;
           tmpLog.Text = searchresult.Count.ToString() + " results found!";
           loader.Visible = false;
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
               return;
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
           try
           {
               currentSelection = onlineUsers.SelectedItem.ToString();
           }
           catch (Exception ex)
           {
               saveException(ex.Message);
               return;
           }
           loader.Visible = true;
           showUserFolders(currentSelection);
       }
       
        /**
         * function to populate the listview with the shared folders of any user
         */ 
       public void showUserFolders(string nname)
       {
           back.Push(nname);
           currentSelectionfolders.Clear();
           crawler.root = @"\\" + nname + @"\";
           currentSelectionfolders = crawler.get_folders(nname);
           presentLocation.Text = crawler.root;

           if (currentSelectionfolders.Count == 0 || currentSelectionfolders[0].ToString() == "-1")
           {
               onlineUserS.Remove(nname);
               populateUserList();
               bottombar_label2.Text = "User is not available or not accessible. User removed from list";
               presentLocation.Text = string.Empty;
           }
           lv_menu_download.Enabled = false;    //disable DOWNLOAD in contextmenu
           workspace.Items.Clear();
           for (int i = 0; i < currentSelectionfolders.Count; i++)
           {
               ListViewItem foo = new ListViewItem(new string[] { currentSelectionfolders[i].ToString(), crawler.getOwner(currentSelectionfolders[i].ToString()), "", crawler.getCategory(currentSelectionfolders[i].ToString()), "" });
               workspace.Items.Add(foo);
               workspace.Items[i].ImageIndex = 0;
           }
           loader.Visible = false;
       
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
           refreshListView(sel); 
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
               refreshListView(sel);
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
<<<<<<< HEAD
           int folderCount = 0,i;
=======
           
           
>>>>>>> parent of 430cbf7... major changes implemented
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
               lv_menu_download.Enabled = true; //enable DOWNLOAD in context menu
               back.Push(sel);
               presentLocation.Text = sel;
               workspace.Items.Clear();
               int len = folders.Length;
               for ( i = 0; i < len; i++)
               {
                   workspace.Items.Add(new ListViewItem(new string[] { folders[i], crawler.getOwner(folders[i]), "", "folder", "--NA--" }));
                   workspace.Items[i].ImageIndex = 0;
               }
               folderCount = i;
               string[] files = Directory.GetFiles(sel);
               len = files.Length;
               for (i = 0; i < len; i++)
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
                       if (crawler.getCategory(files[i]) == "movie/video") workspace.Items[folderCount + i].ImageIndex = 2;
                       if (crawler.getCategory(files[i]) == "image") workspace.Items[folderCount + i].ImageIndex = 4;
                       if (crawler.getCategory(files[i]) == "music") workspace.Items[folderCount + i].ImageIndex = 3;
                       if (crawler.getCategory(files[i]) == "file") workspace.Items[folderCount + i].ImageIndex = 5;
                       if (crawler.getCategory(files[i]) == "app") workspace.Items[folderCount + i].ImageIndex = 1;
                   }
                   catch (Exception ex) 
                   { 
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
               lv_menu_download.Enabled = false;    //disable DOWNLOAD in context menu
               workspace.Items.Clear();
               for (i = 0; i < folders.Count; i++)
               {
                   workspace.Items.Add(new ListViewItem(new string[] { folders[i].ToString(), crawler.getOwner(folders[i].ToString()), "--NA--", "--NA--", "--NA--" }));
                   workspace.Items[0].ImageIndex = 0;
               }
           }
           catch (IOException ex)
           {
               //this means it can be a file
               try
               {
                   openDirec(sel);
               }
               catch (Exception obj)
               {
                   /**
                    * report this to server
                    */ 
                   MessageBox.Show("Unable to open file! Our bad :/");
               }
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
         * also sort of placeholder added
         */ 
       private void search_Click(object sender, EventArgs e)
       {
           if (search.Text == " search here..." ) search.Text = "";
           else search.SelectAll();
       }
       private void search_lostfocus(object sender, EventArgs e)
       {
           if (search.Text == "") search.Text = " search here...";
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
           this.Hide();
           notifICO.BalloonTipTitle = "Minimised to system tray";
           notifICO.BalloonTipText = "You can access lStore from Notification panel anytime. Stay connected!";
           notifICO.ShowBalloonTip(1000);
           System.Threading.Thread.Sleep(3000);
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
           try
           {
               string sel = workspace.SelectedItems[0].Text;
               openDirec(sel);
           }
           catch (ArgumentOutOfRangeException ex)
           {
               MessageBox.Show("We cannot open something that is Nothing :p");
           }
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

       /**
       * when user manually clicks on refresh button
       */ 
       private void refreshbutton1_Click(object sender, EventArgs e)
       {
           bottombar_label1.Text = "Refreshing user list!";
           if (onlineUserRetriever.IsBusy)
           {
               bottombar_label1.Text = "Already refreshing!";
           }
           else
           {
               progressBar1.Visible = true;
               refreshbutton1.Visible = false;
               onlineUserRetriever.RunWorkerAsync();
           }
           
           
       }
        
       /**
        * action when someone clicks on present location textbox
        * action: is to select all text
        */ 
       private void presentLocation_MouseClick(object sender, MouseEventArgs e)
       {
           presentLocation.SelectAll();
       }

        /**
         * to make back button work
         */ 
        private void backbutton_Click(object sender, EventArgs e)
        {

            try
            {
                string temp = back.Pop();
                string backFolder = back.Pop();
                if (backFolder.IndexOf(@"\") == -1)
                {
                    showUserFolders(backFolder);
                }
                else
                {
                    refreshListView(backFolder);
                }
            }
            catch (InvalidOperationException ex)
            {
                /**
                 * cant go any back
                 * this is time to lower opacity of back button or change it
                 */
            }
       }


        /**
         * called when user clicks on copy option in context menu of the 
         * list view
         */ 
        private void cOPYToolStripMenuItem1_Click(object sender, EventArgs e)
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

<<<<<<< HEAD
        /**
         * function to change the ratebutton image on hover and viceversa
         */ 
        private void icon_rate_MouseHover(object sender, EventArgs e)
        {
            icon_rate.BackgroundImage = System.Drawing.Image.FromFile(primaryFolder +@"\img\ratehover.png");
        }
        private void icon_rate_MouseLeave(object sender, EventArgs e)
        {
            icon_rate.BackgroundImage = System.Drawing.Image.FromFile(primaryFolder + @"\img\ratenormal.png");
        }


        /**
         * function to perform directory open task like in case of windows explorer
         * #region1 begins here ======================================================
         */ 
        private void icon_go_Click(object sender, EventArgs e)
        {
            gotoDirectory();
        }

        private void presentLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                gotoDirectory();
            }
        }
        private void presentLocation_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                return;
            }
            else if (presentLocation.Text == @"lStore/" || presentLocation.Text.Length == 0)
            {
                icon_go.Visible = false;
            }
            else
            {
                icon_go.Visible = true;
            }
        }

        /**
         * function to load directory as enered in presentlocation textbox
         * also: * if the added user is not on list then need to add him to that as well
         */
        private void gotoDirectory()
        {
            if (presentLocation.Text == @"lStore/" || presentLocation.Text.Length == 0) return;
            if (Directory.Exists(@presentLocation.Text))
            {
                refreshListView(@presentLocation.Text);
                string[] tmparr = presentLocation.Text.Split('\\');
                currentSelection = tmparr[2];
                viewSelection();             
            }
            else 
            {
                try
                {
                    showUserFolders(@presentLocation.Text.Replace(@"\",""));
                    string[] tmparr = presentLocation.Text.Split('\\');
                    currentSelection = tmparr[2];
                    viewSelection();
                    if (!onlineUserS.Contains(currentSelection))
                    {
                        onlineUserS.Add(currentSelection);
                        populateUserList();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("This location does not exist or is not available now!");
                    presentLocation.SelectAll();
                    hideSelection();
                }
            }
        }

        /**
         * funcion to open
         * preferences form on click
         */ 
        private void icon_preferences_Click(object sender, EventArgs e)
        {
            prefObj.Show();
        }

        /**
         * #region1 ends here =============================================================
         */

        /**
         * function to go to root directory of user when he clicks on this icon
         * #region3 starts here ==============================================
         */
        private void icon_user_Click(object sender, EventArgs e)
        {
            presentLocation.Text = user_label.Text;
            gotoDirectory();
        }

        private void user_label_Click(object sender, EventArgs e)
        {
            presentLocation.Text = user_label.Text;
            gotoDirectory();
        }

        private void icon_preferences_MouseHover(object sender, EventArgs e)
        {
            //tooltip_preferences.Show("Click here to change lStore Settings",this);
            //tooltip_preferences.Show("Click here to change lStore Settings", this, int.Parse(eve.X.ToString()),int.Parse(eve.Y.ToString()),1000);
            ////tT.Show("Why So Many Times?", this);
        }
        /**
         * #region3 sends here ==============================================
         */

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            notifICO.BalloonTipText = "You can access lStore from Notification panel anytime. Stay connected!";
            notifICO.ShowBalloonTip(1000);
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            preferences obj = new preferences();
            obj.Show();
        }


        /**
         * function to switch the listview style
         */
        private int listviewView = 0;
        private void icon_switchlistview_Click(object sender, EventArgs e)
        {
            if (listviewView == 0)
            {
                listviewView++;
                workspace.View = System.Windows.Forms.View.List;
                icon_switchlistview.BackgroundImage = System.Drawing.Image.FromFile(primaryFolder + @"\img\detail.png");
            }
            else 
            {
                listviewView = 0;
                workspace.View = System.Windows.Forms.View.Details;
                icon_switchlistview.BackgroundImage = System.Drawing.Image.FromFile(primaryFolder + @"\img\list.png");
            }
        }

        /**
         * code to donwload a certain thing to download directory
         */ 
        private void lv_menu_download_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(prefObj.downloadDirectory))
                {
                    Directory.CreateDirectory(prefObj.downloadDirectory);
                }

                /**
                 * copy to clipboard
                 */
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

                /**
                 * paste to directory
                 */
            }
            catch (ArgumentNullException ex) {
                MessageBox.Show("Cannot download NULL content");
            }

        }

=======
>>>>>>> parent of 430cbf7... major changes implemented
  
   }
}
