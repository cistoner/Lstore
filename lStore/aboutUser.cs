using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
namespace lStore
{
    public partial class aboutUser : Form
    {
        public string userName = userInfo.username, localName = userInfo.networkname;
        public string primaryFolder = @"C:\Users\" + userInfo.username + @"\Documents\lStore";
        public aboutUser()
        {
            InitializeComponent();
            rating.Text = "[STARS] " + userInfo.rating + "";
            codeLocation.Text = "[LOC] " + userInfo.location;
            countFilesShared.Text = "[FILES] " + userInfo.files_shared + "";
            uname.Text = "" + userName;
            nname.Text = @"\\" + localName;
            if (File.Exists(@"C:\Users\" + userName + @"\Documents\lStore\user.jpg"))
            {
                profilepic.Image = System.Drawing.Image.FromFile(@"C:\Users\" + userName + @"\Documents\lStore\user.jpg");
            }
        }

        private void aboutUser_Load(object sender, EventArgs e)
        {

        }
    }
}
