using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace lStore
{
    class onlineUser
    {
        public static string primaryFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\lStore";
        public static string filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +@"\lStore\tmp\searchedUsers_.log";

        /**
         * constructor: 
         * task:- to get list of online ips 
         */ 
        public onlineUser()
        { 
            
        }

        /**
         * function to run command prompt command
         * arp -g
         * and ping all resulting IP address to check for their name
         * and activity status
         */
        public static void getIpList()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "arp";
            startInfo.Arguments = "-g";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();
            String strData = process.StandardOutput.ReadToEnd();
            Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            MatchCollection result = ip.Matches(strData);
            foreach (Match r in result)
            {
                Ping p = new Ping();
                p.PingCompleted += new PingCompletedEventHandler(p_PingCompleted);
                p.SendAsync(r.ToString(), 100, r.ToString());
            }
        }

        /**
         * function to act as event listner to the pings sent to 
         * different systems retrieved by thr ARP -g command
         */
        public static void p_PingCompleted(object sender, PingCompletedEventArgs e)
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
                        /**
                         * considering IP address is not needed
                         */
                        name = hostEntry.HostName;
                        File.AppendAllText(primaryFolder + @"\tmp\alluserlist.data", name + Environment.NewLine);
                    }
                    catch (SocketException ex) { }
                }
                catch (Exception ex) { }
            }
        }
        
    }
}
