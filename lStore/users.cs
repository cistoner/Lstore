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
                p.SendAsync(ip, 100, ip);
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
                        File.AppendAllText(primaryFolder + @"\tmp\tmp.data", name +"*" +ip + Environment.NewLine);
                    }
                    catch (SocketException ex){}
                }
                catch (Exception ex){}
            }
        }

    }
}
