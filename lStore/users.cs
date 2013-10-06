using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Net.NetworkInformation;
using System.Threading;

namespace lStore
{
    class users
    {
        private ArrayList onlineUser = new ArrayList();
        private ArrayList onlineUserIp = new ArrayList();
        public int onlineUsercount;
        public string primaryFolder;
        public static string static_primaryFolder = @"C:\Users\" + Environment.UserName + @"\Documents\lStore";
        public string baseaddr;
        /*
         * a function to check the users available on LAN
         */
        public users(string baseaddress,string folder) {
            primaryFolder = folder;
            baseaddr = baseaddress;
            pingUsers(baseaddr);
        }
        public void pingUsers(string addr)
        {
            File.WriteAllText(primaryFolder + @"\tmp\tmp.data", "");
            onlineUser.Clear();
            onlineUserIp.Clear();
            onlineUsercount = 0;
            for (int i = 0; i <= 255; i++)
            {
                string ip = (string)addr + "." + i.ToString();
                Ping p = new Ping();
                p.PingCompleted += new PingCompletedEventHandler(p_PingCompleted);
                try
                {
                    p.SendAsync(ip, 100, ip);
                }
                catch (PingException ex) 
                { 
                    /* take some actions here */
                }
            }
            Thread.Sleep(30000);
            copyOnlineUserFile();   //time set to 5 seconds
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
                        File.AppendAllText(primaryFolder + @"\tmp\tmp.data", name +"*" +ip + Environment.NewLine);
                    }
                    catch (SocketException ex){}
                }
                catch (Exception ex){}
            }
        }
        /*
         * a function to return arraylist of name of online user from file
         */
        public static ArrayList getUsers()
        {
            string filename = static_primaryFolder + @"\tmp\online.data";
            ArrayList returnAL = new ArrayList();
            try
            {
                string[] tmp = File.ReadAllLines(filename);
                for (int i = 0; i < tmp.Length; i++)
                {
                    string[] arr = tmp[i].Split('*');
                    returnAL.Add(arr[0]);
                }
            }
            catch (FileNotFoundException ex)
            {
                File.WriteAllText(filename,"");
                returnAL.Add("ALL OFFLINE");

            }
           
            return returnAL;
        }
        /*
         * a function to return arraylist of ips of online user from file
         */
        public static ArrayList getUserIp()
        {
            string filename = static_primaryFolder + @"\tmp\online.data";
            string[] tmp = File.ReadAllLines(filename);
            ArrayList returnAL = new ArrayList();
            for (int i = 0; i < tmp.Length; i++)
            {
                string[] arr = tmp[i].Split('*');
                returnAL.Add(arr[1]);
            }
            return returnAL;
        }
        /* function to explicitly copy contents of
         * tmp.data to online.data
         */
        private void copyOnlineUserFile()
        { 
            string destination = primaryFolder + @"\tmp\online.data";
            string source = primaryFolder + @"\tmp\tmp.data";
            string data = File.ReadAllText(source);
            File.WriteAllText(destination,data);
            return;
        }
        

    }
}
