using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net.NetworkInformation;
using System.Net;

namespace lStore
{
    class userInfo
    {
        public static string defaultGateway = "192.168.100.1";
        public static string ipaddress;
        public static string baseaddress;
        public static string macAddress;                                 // mac address of the device
        public static string username = Environment.UserName;            // device username
        public static string networkname = Environment.MachineName;         //network username
        //public static string sysManufacturer ;
        /* need to get code to fetch this */
        public static string ram;                                       //total user ram 
        public static int cores = Environment.ProcessorCount;           //total no of logical cores
        //public static string processors;
        /* need to get code to fetch this */
        public static string resolution;                                //computer screen resolution in W x H
        public static string osInfo;                                    //computer's os detail

        /* 
         *  function to get
         *  all the data which cannot be retrieved from environment variables
         *  must be called before the values are used
         */ 
        public static void getAllData()
        {
            ram = new  Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory.ToString();
            osInfo = new Microsoft.VisualBasic.Devices.ComputerInfo().OSFullName.ToString();
            resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Right.ToString() + " X " + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Bottom.ToString();
            macAddress = GetMacAddress().ToString();
            baseaddress = getBaseAddress(defaultGateway);    //method to get the baseaddress
            getIpAddress();     //method to get ip address
            
        }
        /* 
         * fnction to get the mac address of the device
         */ 
        public static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }
        /*
         * a function to get base address out of default parameter ip
         * @param: string ip: any ip address
         */
        public static string getBaseAddress(string ip)
        {
            string[] parts = ip.Split('.');
            return parts[0] + '.' + parts[1] + '.' + parts[2];
        }
        /*
         * function to get ip address of the system
         */
        public static void getIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry myself = Dns.GetHostByName(hostName);
            foreach (IPAddress address in myself.AddressList)
            {
                if (getBaseAddress(address.ToString()) == baseaddress)
                {
                    ipaddress = address.ToString();
                }
            }
        }
        
        


    }
}
