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
using System.Net.WebSockets;
using System.Net.NetworkInformation;
//=========================================namespaces till here==============
namespace lStore
{
    public partial class lStore : Form
    {
        public lStore()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //task here is to check if this is first time or not
            if (isFirstTime())
            { 
                //this means this is its first time
                MessageBox.Show(" Dear user, this is the first time you are using this app!\n We will now register you to our server so that you can use the app without any flaws. \nThanks for downloading this software!!");
            }
        }
        /*
         * this function checks if this is first time user is using this app
         * parameters: null
         * return type: bool
         */ 
        public bool isFirstTime()
        { 
            //return true;
            return false;
        }
    }
}
