using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace lStore
{
    public partial class preferences : Form
    {
        public string primaryFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\lStore";
        public string defaultDownloaddirec = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\lStoreDownloads";
        public string xmlfile, xmldata;

        /**
         * variables to hold different values
         * #region1 starts here ========================
         */
        public int imdb, searchsuggestion;
        public int crawlFreq, syncFreq, statsFreq;
        public int searchStats, bugsStats, internetusageStats, usageStats;
        public int useOtherInternet;
        public string downloadDirectory;
        /**
         * #region1 ends here ========================
         * variable to hold different values
         */ 
        //private XmlReader reader;

        public preferences()
        {
            InitializeComponent();
            xmlfile = primaryFolder + @"\settings.ini";
            if (!File.Exists(xmlfile)) { repariPreferences(); }
            xmldata = File.ReadAllText(xmlfile);
            
        }

        private void preferences_Load(object sender, EventArgs e)
        {
           /**
            * code to load preferences from xml
            */
            searchsuggestion = int.Parse(getDataFromXML("searchsuggestion"));
            imdb = int.Parse(getDataFromXML("imdb"));
            crawlFreq = int.Parse(getDataFromXML("crawl"));
            syncFreq = int.Parse(getDataFromXML("sync"));
            statsFreq = int.Parse(getDataFromXML("stats"));
            searchStats = int.Parse(getDataFromXML("search"));
            bugsStats = int.Parse(getDataFromXML("bugs"));
            internetusageStats = int.Parse(getDataFromXML("internetusage"));
            usageStats = int.Parse(getDataFromXML("usage"));
            useOtherInternet = int.Parse(getDataFromXML("useotherinternet"));
            downloadDirectory = getDataFromXML("download");
            /**
             * checking the form checkboxes depending upon values in xml
             */ 
            if (imdb == 1)
            {
                addon_imdb.Checked = true;
            }
            if (searchsuggestion == 1)
            {
                addon_searchsuggestion.Checked = true;
            }
            if( bugsStats == 1)
            {
                stats_bugs.Checked = true;
            }
            if( internetusageStats == 1)
            {
                stats_internetusage.Checked = true;
            }
            if( usageStats == 1)
            {
                stats_usage.Checked = true;
            }
            if( searchStats == 1)
            {
                stats_search.Checked = true;
            }
            if (crawlFreq != 0)
            {
                freq_crawl.Value = crawlFreq;
            }
            if (syncFreq != 0)
            {
                freq_sync.Value = syncFreq;
            }
            if (statsFreq != 0)
            {
                freq_upload.Value = statsFreq;
            }
            if (useOtherInternet == 1)
            {
                check_internet.Checked = true;
            }
            input_downloaddirec.Text = downloadDirectory;


            
        }

        /**
         * function to get certain value from xml
         * and return its value
         */ 
        private string getDataFromXML(string node)
        {
            /**
             * currently for each run an instance of xml reader is created
             * change it with one global instance and parsing it in different 
             * calls
             */ 
            try
            {
                XmlReader reader = XmlReader.Create(new StringReader(xmldata));
                reader.ReadToFollowing(node);
                string output = reader.ReadElementContentAsString();
                return output;
            }
            catch (Exception ex) { return "0"; }
        }

        /**
         * function to recreate the preferences file if its damages or not found
         */ 
        private void repariPreferences()
        {
            string xml = "<?xml version='1.0' encoding='UTF-8'?>" + Environment.NewLine + "<settings>";
            xml += "<imdb>1</imdb>" + Environment.NewLine;
            xml += "<searchsuggestion>1</searchsuggestion>" + Environment.NewLine;
            xml += "<crawl>720</crawl>" + Environment.NewLine;
            xml += "<sync>720</sync>" + Environment.NewLine;
            xml += "<stats>1440</stats>" + Environment.NewLine;
            xml += "<search>1</search>" + Environment.NewLine;
            xml += "<usage>1</usage>" + Environment.NewLine;
            xml += "<bugs>1</bugs>" + Environment.NewLine;
            xml += "<internetusage>1</internetusage>" + Environment.NewLine;
            xml += "<useotherinternet>0</useotherinternet>" + Environment.NewLine;
            xml += "<download>" + defaultDownloaddirec + "</download>" + Environment.NewLine;
            xml += "</settings>" + Environment.NewLine;
            File.WriteAllText(xmlfile, xml);
            if (!Directory.Exists(defaultDownloaddirec))
            {
                Directory.CreateDirectory(defaultDownloaddirec);
            }
        }

        /**
         * function to change 1 -> 0 and 0 -> 1
         */
        private int flip(int x)
        {
            if (x == 0) return ++x;
            else return --x;
        }

        /**
         * function to close the preferences form when one click on cancel button
         */
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /**
         * #region2: checkedChange functions start here ======================
         */ 
        private void stats_search_CheckedChanged(object sender, EventArgs e)
        {
            searchStats = (stats_search.Checked) ? 1 : 0;
        }

        private void stats_usage_CheckedChanged(object sender, EventArgs e)
        {
            usageStats = (stats_usage.Checked) ? 1 : 0;
        }

        private void stats_bugs_CheckedChanged(object sender, EventArgs e)
        {
            bugsStats = (stats_bugs.Checked) ? 1 : 0;
        }

        private void stats_internetusage_CheckedChanged(object sender, EventArgs e)
        {
            internetusageStats = (stats_internetusage.Checked) ? 1 : 0;
        }

        private void freq_crawl_ValueChanged(object sender, EventArgs e)
        {
            crawlFreq = int.Parse(freq_crawl.Value.ToString());
        }

        private void freq_sync_ValueChanged(object sender, EventArgs e)
        {
            syncFreq = int.Parse(freq_sync.Value.ToString());
        }

        private void freq_upload_ValueChanged(object sender, EventArgs e)
        {
            statsFreq = int.Parse(freq_upload.Value.ToString());
        }

        private void addon_imdb_CheckedChanged(object sender, EventArgs e)
        {
            imdb = (addon_imdb.Checked) ? 1 : 0;
        }

        private void addon_searchsuggestion_CheckedChanged(object sender, EventArgs e)
        {
            searchsuggestion = (addon_searchsuggestion.Checked) ? 1 : 0;
        }

        private void check_internet_CheckedChanged(object sender, EventArgs e)
        {
            useOtherInternet = (check_internet.Checked) ? 1 : 0;
        }

        

        /**
         * #region2: checkedChange functions ends here ======================
         */
        private void button_apply_Click(object sender, EventArgs e)
        {
            try
            {
                savepreferencetoXML();
                donelabel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save!");
                donelabel.Visible = false;
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                savepreferencetoXML();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save!");
            }
        }

        /**
         * function to save the details in memory to xml
         */ 
        private void savepreferencetoXML()
        { 
            string xml = "<?xml version='1.0' encoding='UTF-8'?>" +Environment.NewLine +"<settings>";
            xml += "<imdb>"+imdb +"</imdb>" +Environment.NewLine;
            xml += "<searchsuggestion>" +searchsuggestion +"</searchsuggestion>" + Environment.NewLine;
            xml += "<crawl>" + crawlFreq + "</crawl>" + Environment.NewLine;
            xml += "<sync>" + syncFreq + "</sync>" + Environment.NewLine;
            xml += "<stats>" + statsFreq + "</stats>" + Environment.NewLine;
            xml += "<search>" + searchStats + "</search>" + Environment.NewLine;
            xml += "<usage>" + usageStats + "</usage>" + Environment.NewLine;
            xml += "<bugs>" + bugsStats + "</bugs>" + Environment.NewLine;
            xml += "<internetusage>" + internetusageStats + "</internetusage>" + Environment.NewLine;
            xml += "<useotherinternet>" + useOtherInternet + "</useotherinternet>" + Environment.NewLine;
            xml += "<download>" + downloadDirectory + "</download>" + Environment.NewLine;
            xml += "</settings>" + Environment.NewLine;
            File.WriteAllText(xmlfile,xml);
        }

        /**
         * code to open a dialog box select a directory and change value of download directory
         */ 
        private void browsebutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            downloadDirectory = fbd.SelectedPath.ToString();
            input_downloaddirec.Text = downloadDirectory;
        }
    }
}
