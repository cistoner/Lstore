using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

/**
 * class to upload stats to server
 * depending upon preferences
 */ 
namespace lStore
{
    class uploader
    {
        private static preferences prefObj = new preferences();
        private static bool searchUpl = false, bugsUpl = false, internetusageUpl = false, usageUpl = false;
        private static string url = "http://localhost/lstore/";
        public static string primaryFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\lStore";
        
        /**
         * constructor to load the values
         */ 
        uploader()
        {
            if (prefObj.internetusageStats == 1)
                internetusageUpl = true;
            if (prefObj.searchStats == 1)
                searchUpl = true;
            if (prefObj.usageStats == 1)
                usageUpl = true;
            if (prefObj.bugsStats == 1)
                bugsUpl = true;
        }

        public static void uploadStats()
        {
            if (internetusageUpl)
            { 
                //post the data to respective url.
                //also send hash id
            }
            
            if (searchUpl)
            {
            
            }
            
            if (usageUpl)
            { 
            
            }

            if (bugsUpl)
            { 
            
            }
        }

        /**
         * function to send post request to any page and retrieve the content
         */
        public static string SendPost(string url, string postData)
        {
            string webpageContent = string.Empty;

            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;

                using (Stream webpageStream = webRequest.GetRequestStream())
                {
                    webpageStream.Write(byteArray, 0, byteArray.Length);
                }

                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        webpageContent = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                //throw or return an appropriate response/exception
            }
            return webpageContent;
        }
    }
}
