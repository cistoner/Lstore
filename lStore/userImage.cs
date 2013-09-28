using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace lStore
{
    class userImage
    {
        public string userName = Environment.UserName;
        public int width = 103, height = 123;   //default size of picture box for now
        /*
         * code to create a copy of selected image in
         * lStore temp directory
         */ 
        public string moveImage(string fileToOpen)
        {
            string[] splitname = fileToOpen.Split('.');
            string extension = splitname[(splitname.Length - 1)].ToLower();
            string profileImageDirec = @"C:\Users\" + userName + @"\Documents\lStore\tmp\user." + extension;
            if (extension == "jpg" || extension == "png" || extension == "bmp" || extension == "jpeg")
            {
                //System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);
                //System.IO.StreamReader reader = new System.IO.StreamReader(fileToOpen);
                if (File.Exists(profileImageDirec)) File.Delete(profileImageDirec);
                try
                {
                    File.Copy(fileToOpen, profileImageDirec);
                }
                catch (DirectoryNotFoundException ex)
                {
                    //repairFolders();
                    /* alternative to this has to be found */
                    File.Copy(fileToOpen, profileImageDirec);
                }
                return profileImageDirec;
            }
            return "-1";
        }
        /*
         * function to return extension of a file
         * @param: filename
         * return : string extension
         */ 
        public string getExtension(string filename)
        {
            string[] splitname = filename.Split('.');
            string extension = splitname[(splitname.Length - 1)].ToLower();
            return extension;
        }
        /*
         * funtion to generate  thumbnail of any image file
         * return type: void
         * @param: source image name and destination image name
         */ 
        public void GenerateThumbNail(string sourcefile, string destinationfile)
        {
            File.Delete(destinationfile);
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourcefile);
            int srcWidth = image.Width;
            int srcHeight = image.Height;
            int thumbWidth = width;
            int thumbHeight = height;
            Bitmap bmp;
            bmp = new Bitmap(thumbWidth, thumbHeight);
            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            System.Drawing.Rectangle rectDestination =
                   new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
            gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
            bmp.Save(destinationfile);
            bmp.Dispose();
            image.Dispose();
            File.Delete(sourcefile);
        }
    }
}
