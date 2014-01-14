using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.IO;
using System.Collections;

/**
 * task of the class
 * #1 sync from server to client based on sync level
 * #2 functions for background crawling which run smoothly based on preferences
 */ 
namespace lStore
{
    class synchronise
    {
        /**
         * variable to store current sync level
         */ 
        public int synclevel;

        /**
         * variable to stroe default gateway id
         */ 
        public int defaultgatewayId;

        /**
         * base url to which all pings are made
         */ 
        private string url = "http://localhost/lstore/";
        private Queue<string> filequeue;
        /**
         * connection string for database connection
         */ 
        private string connectionstring = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\q4sp9x.mdf;Integrated Security=True;Connect Timeout=300";
        
        public string primaryFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\lStore";
        private bool isProxyEnabled = false;

        /**
         * constructor
         * #tasks#
         * #1 get current sync level for present default gateway
         * #2 check for internet connection
         */ 
        synchronise()
        {
            string defaultGateway = userInfo.defaultGateway;
            using (SqlConnection mycon = new SqlConnection(connectionstring))
            {
                try
                {
                    string sqlSelect = "SELECT synclevel,Id FROM defaultGateway WHERE IP = @paramdg";
                    SqlCommand sqlC = new SqlCommand(sqlSelect, mycon);
                    sqlC.Parameters.Add(new SqlParameter("paramdg", defaultGateway));
                    mycon.Open();
                    SqlDataReader r = sqlC.ExecuteReader();
                    if (r.HasRows)
                    {
                        while (r.Read())
                        {
                            synclevel = int.Parse(r["synclevel"].ToString());
                            defaultgatewayId = int.Parse(r["Ip"].ToString());
                        }
                    }
                }
                catch (SqlException ex) { }
            }

            /**
             * to recreate logging files
             */ 
            File.WriteAllText(primaryFolder + @"\tmp\notinserted.data", "");
            File.WriteAllText(primaryFolder + @"\tmp\sync.log", "");
        }

        
        /**
         * function to 
         * #1 sync the client with server
         * #2 update the sync level to db
         */ 
        public void onlinetolocalSync()
        {
            string data = SendPost(url + "generalsync.php", "hash=" + userInfo.hash + "&dg=" + userInfo.defaultGateway + "&synclevel=" + synclevel);
            string[] rows = data.Split('\n');
            int newsynclevel = int.Parse(rows[0]);

            SqlConnection mycon = new SqlConnection(connectionstring);
            var command = mycon.CreateCommand(); 
            mycon.Open();

            /**
             * code to synchronise
             */ 
            for (int i = 1; i < rows.Length; i++)
            {
                string row = rows[i].Replace("-_-",@"\");
                string[] arr = row.Split('\\');
                try
                {
                    command.CommandText = "INSERT INTO files (Id,filename,keywords,dg_id) VALUES (@param" + i.ToString() + "1,@param" + i.ToString() + "2,@param" + i.ToString() + "3,@param" + i.ToString() + "4)";
                    command.Parameters.AddWithValue("param" + i.ToString() + "1", int.Parse(arr[0]));        //file id
                    command.Parameters.AddWithValue("param" + i.ToString() + "2", arr[1]);                   //filename
                    command.Parameters.AddWithValue("param" + i.ToString() + "3", arr[2].ToString());        //keywords
                    command.Parameters.AddWithValue("param" + i.ToString() + "4", defaultgatewayId);
                    int countrow = command.ExecuteNonQuery();
                    if (countrow != 1)
                    {
                        File.AppendAllText(primaryFolder + @"\steps.txt", "INSERTION FAILED FOR ELEMENT_ NORMALSYNC_ " + i.ToString() + Environment.NewLine);
                    }
                }
                catch (SqlException ex) { }
                catch (Exception ex)
                {
                    File.AppendAllText(primaryFolder + @"\tmp\notinserted.data", rows[i] + Environment.NewLine);
                    if (ex.Message.IndexOf("UQ__files_") == -1)
                    {
                        File.AppendAllText(primaryFolder + @"\tmp\sync.log", ex.Message + Environment.NewLine);
                    }
                }

                /**
                 * to make the system smooth in background
                 */ 
                System.Threading.Thread.Sleep(1);
            }
            mycon.Close();

            /**
             * code to update local synclevel
             */
            mycon.Open();
            /*
             * error handeling shalll be added after test
             */ 
            string sqlSelect = "UPDATE defaultGateway SET synclevel = @paramsl WHERE IP = @paramdg_";
            SqlCommand sqlC = new SqlCommand(sqlSelect, mycon);
            sqlC.Parameters.Add(new SqlParameter("paramdg_", userInfo.defaultGateway));
            sqlC.Parameters.Add(new SqlParameter("paramsl", newsynclevel));
            sqlC.ExecuteNonQuery();
            mycon.Close();

        }

        /**
         * function to do general crawling once system is idle and
         * match each one of the entries with the one db and prepare a 
         * list of new files on LAN
         */ 
        public void generalCrawl()
        {
            generalCrawlSelf();
            generalCrawlAll();
        }

        /**
         * function to crawl and compare self files
         */ 
        public void generalCrawlSelf()
        {
            //need to fininsh
            string nname = userInfo.networkname;
            ArrayList folders = crawler.get_folders(nname);
            filequeue.Clear();
            foreach (string folder in folders)
            {
                recursive_folder_data_get(folder);
            }
            //now match this with db
            matchagainstDB(true);
        }

        /**
         * function to recursively go through files and store it in memory
         */ 
        private void recursive_folder_data_get(string folder)
        {
            string[] folders = Directory.GetDirectories(folder);
            for (int i = 0; i < folders.Length; i++)
            {
                recursive_folder_data_get(folders[i]);
            }
            string[] files = Directory.GetFiles(folder);
            for (int i = 0; i < files.Length; i++)
            {
                filequeue.Enqueue(files[i]);
                string file_new = files[i].Replace(@"\", "~");
            }
        }

        /**
         * function to match filelist in queue and db and save new filenames
         * to files
         * @param: to specify if this is about self user or other user
         */
        private void matchagainstDB(bool self = false)
        {
            string exportFile;
            if (!self) exportFile = primaryFolder + @"\tmp\syncfile_all";
            else exportFile = primaryFolder + @"\tmp\syncfile_self";
            bool done = false;
            while (!done)
            {
                try
                {
                    /**
                     * code to match against all elements
                     */ 
                }
                catch (Exception ex)
                {
                    //when queue becomes empty
                    done = true;
                }
            }
        }
        
        /**
         * function to crawl and compare every other 
         * system except self
         */ 
        public void generalCrawlAll()
        {
            //need to fininsh
            filequeue.Clear();

        }

        /**
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
