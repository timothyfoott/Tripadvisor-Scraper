using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Globussoft.File;
using GlobusLib;

namespace X509Query
{
    public abstract class X509Query
    {
        private readonly X509Certificate certificate;

        protected X509Query(X509Certificate cert)
        {
            certificate = cert;
        }

        public string SubmitQuery(string url)
        {
            //Create Request and Send
            HttpWebRequest request = CreateRequest(url);

            //Wait for Response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //Read Response
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            return reader.ReadToEnd();
        }

        private HttpWebRequest CreateRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "text/xml,text/html; charset=iso-8859-1";
            request.Method = "POST";

            //Add Cert to Request
            request.ClientCertificates.Add(certificate);

            //Get Data to Post
            string xml = GetPostData();

            //Set Content Length
            byte[] xmlBytes = Encoding.ASCII.GetBytes(xml);
            request.ContentLength = xmlBytes.Length;

            //Send Request
            Stream s = request.GetRequestStream();
            s.Write(xmlBytes, 0, xmlBytes.Length);

            return request;
        }

        protected abstract string GetPostData();



    }
    public class MyQuery : X509Query
    {
        public MyQuery(X509Certificate cert)
            : base(cert)
        {
        }

        protected override string GetPostData()
        {
            GlobusHttpHelper httphelper = new GlobusHttpHelper();
             string yahoopge = httphelper.getHtmlfromUrl(new Uri("https://login.yahoo.com/config/login?.done=http://answers.yahoo.com%2findex&.src=knowsrch&.intl=us"));
            string splitstring = ((yahoopge.Substring(yahoopge.IndexOf("onsubmit=") + 30, yahoopge.IndexOf("form: login information") - (yahoopge.IndexOf("onsubmit=") + 60))).Replace("\n", "").Replace("\t", "").Replace("<input type=\"hidden\" name=", "&").Replace(">", "").Replace("value", "").Replace("\"", "") + "&login=" + "manish_patel226" + "&passwd=" + "man_007" + "&.save=Sign+In").Replace(" ", "").Replace("index", "");
            splitstring = splitstring.Substring(1, splitstring.Length - 1).Replace(":", "%3A").Replace("/", "%2F").Replace("knowsrch_ver=0&c=&ivt=&sg=", "knowsrch_ver%3D0%26c%3D%26ivt%3D%26sg%3D");
            return splitstring;
 
            
           
        }
    }

}
