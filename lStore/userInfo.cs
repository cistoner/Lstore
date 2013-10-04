using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace lStore
{
    class userInfo
    {
        public static string defaultGateway;
        public static string ipaddress;
        public static string baseaddress;
        public static string macAddress;
        public static string username = Environment.UserName;            // device username
        public static string networkname = Environment.MachineName;
        public static string sysManufacturer ;
        public static string ram;                                       //total user ram 
        public static int cores = Environment.ProcessorCount;           //total no of logical cores
        public static string processors;
        public static string resolution;    
        public static string osInfo;                                    //computer's os detail
        /*
         * function to get all data of a static variable
         */ 
        public static void getAllData()
        {
            ram = new  Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory.ToString();
            osInfo = new Microsoft.VisualBasic.Devices.ComputerInfo().OSFullName.ToString();
            resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Right.ToString() + " X " + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Bottom.ToString();
            
        }



    }
}
