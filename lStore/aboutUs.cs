using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lStore
{
    public partial class aboutUs : Form
    {
        public aboutUs()
        {
            InitializeComponent();
        }

        private void aboutUs_Load(object sender, EventArgs e)
        {
            about.Text = "This tool allows you to anonymously or publicly share contents over LAN. Clients can download contents directly from you.\n";
            about.Text += "Contents can be searched based on \n\tType:movies,games,songs\n\tRating: for example inside movies,";

        }
    }
}
