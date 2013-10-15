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
         * this function reads the online user from file
         * and return its arraylist
         * back to caller
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
                    returnAL.Add(tmp[i]);
                }
            }
            catch (FileNotFoundException ex)
            {
                File.WriteAllText(filename,"");
                returnAL.Add("ALL OFFLINE");

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
