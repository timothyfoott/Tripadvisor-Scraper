using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Windows.Forms;
using Globussoft.File;


namespace readyahoofilesfromweb
{
    class readfilefromweb
    {
        string UserDetails;
    
        public string Readfiles()
        {
          string FileName = Application.CommonAppDataPath + "\\yahoo_Details.txt";
          if (File.Exists(FileName))
            {
                UserDetails = GlobusFileHelper.ReadStringFromTextfile(FileName);
            }
            return UserDetails;
        }

        public List<string> ReadSearchfile(string clientDirectory)
        {
            string clientsearchlist;
            List<string> keyword_to_search = new List<string>();
            clientsearchlist = "http://www.waitchef.com/yahoo/" + clientDirectory + "/search.txt";
            try
            {
                Uri uri = new Uri(clientsearchlist);
                WebRequest wrequest = WebRequest.Create(uri);
                WebResponse wresponse = wrequest.GetResponse();
                if (!wresponse.ResponseUri.ToString().Contains("clicks/blank.php"))
                {
                    System.IO.Stream wstream = wresponse.GetResponseStream();
                    System.IO.StreamReader sreader = new System.IO.StreamReader(wstream);
                    do
                    {
                        keyword_to_search.Add(sreader.ReadLine());
                    } while (sreader.Peek() != -1);
                   
                }
                else
                {
                    MessageBox.Show("invalid directory", "warning");
             
                }
            
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            
            }
            return keyword_to_search;
        }

        public List<string> ReadSpamfile(string clientDirectory)
        {
            string clientspamlist;
            List<string> spam_list_to_search = new List<string>();
            clientspamlist = "http://www.waitchef.com/yahoo/" + clientDirectory + "/spamurls.txt";
            try
            {
                Uri suri = new Uri(clientspamlist);
                WebRequest swrequest = WebRequest.Create(suri);
                WebResponse swresponse = swrequest.GetResponse();
                if (!swresponse.ResponseUri.ToString().Contains("clicks/blank.php"))
                {
                    System.IO.Stream swstream = swresponse.GetResponseStream();
                    System.IO.StreamReader ssreader = new System.IO.StreamReader(swstream);
                    do
                    {
                        spam_list_to_search.Add(ssreader.ReadLine());
                    } while (ssreader.Peek() != -1);
                }
                else
                {
                    MessageBox.Show("invalid directory", "warning");
                }
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return spam_list_to_search;
        }

       
    }
}