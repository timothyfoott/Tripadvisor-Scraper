using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Windows.Forms;
using Globussoft.File;
using GlobusLib;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using X509Query;

namespace CrawlLinkToSearch
{
    class CrawlLink
    {
        CookieCollection gCookies;
        GlobusHttpHelper httphelper = new GlobusHttpHelper();
        public  List<string> spamlinks = new List<string>();
        public List<string> FetchLinksToSearch(string yahoosearchpage)
        {
            bool nextbuttonstatus=false;
            string nextpagelink=yahoosearchpage;
            List<string>spamtemplist = new List<string>();
            bool currentpagefound = false;
            try
            {
                do
                {
                    nextbuttonstatus = false;
                    currentpagefound = false;
                    string yahoopge = httphelper.getHtmlfromUrl(new Uri(nextpagelink));

                    string[] nextpagelinks = Regex.Split(yahoopge, "<a");
                    nextpagelink = "";
              
                    foreach (string links in nextpagelinks)
                    {
                        try
                        {
                            if (links.Contains("/question/index"))
                            {
                                string link = Regex.Replace(links, "  ", "").Replace("\t", "").Replace("\n", "");
                                link = link.Substring(8, link.IndexOf("\">") - 8);
                                spamtemplist.Add("http://answers.yahoo.com/" + link);
                               
                            }
                            if (links.Contains("/search/search_result"))
                            {
                                if (nextpagelink == "" && currentpagefound == true && links.Contains(";page")&&links.Contains("rel=\"next\" href=\"/search/search_result"))
                                {
                                    nextpagelink = ("http://answers.yahoo.com" + links.Substring(18, links.IndexOf("\">") - 18)).Replace("&amp","&").Replace(";p","p");
                                    nextbuttonstatus = true;
                                }
                            }
                            if (links.Contains(" class=\"current"))
                            {
                                currentpagefound = true;
                            }
                        }
                        catch { Console.WriteLine("if condition error"); }
                    }
                } while (nextbuttonstatus != false);
            }
            catch { Console.WriteLine("error in do whil loop"); 
            }
            return spamtemplist;
        }
        public List<string> checkpagforspam(List<string> spamstring, List<string> spampagetocheck)
        {
            string templink;
            foreach (string spamlist in spampagetocheck)
            {
                break;
                try
                {
                    string pagetocheckspam = httphelper.getHtmlfromUrl(new Uri(spamlist));
                    string[] checkpage = Regex.Split(pagetocheckspam, "<div class=\"qa-container\">");

                    foreach (string spamtext in spamstring)
                    {

                        try
                        {
                            foreach (string page in checkpage)
                            {
                                try
                                {
                                    if (page.Contains(spamtext))
                                    
                                    {
                                        try
                                        {
                                            string[] abuselink = Regex.Split(page, "<li class=\"flag-abuse\">");
                                            templink = "http://answers.yahoo.com" + (abuselink[1].Substring(abuselink[1].IndexOf("href=") + 6, abuselink[1].IndexOf("\">") - (abuselink[1].IndexOf("href=")+33)));
                                            string temp = templink.Replace("&amp;", "&");
                                            Console.WriteLine(temp);
                                            temp = temp.Replace("&link=mailto\">Email</a> <", " ");
                                            if (!page.Contains("<span>Report Abuse</span>"))
                                            {
                                                spamlinks.Add(temp);
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("page contain error");
                                        }

                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Loop error check loop");
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("fetching link fail");
                        }
                    }
                }
                catch { MessageBox.Show("geting the html code of url is fail check your internet connection "); }
            }
            return spamlinks;
        }
        string responseFromServer;
        public void abusepageclick(string postdatauri,string yahooid,string yahoopassword)
        {
            string abusepagelink = postdatauri.Substring(postdatauri.IndexOf("qid"), postdatauri.IndexOf("crumb") + 6 - postdatauri.IndexOf("qid")) + "G6.7YfEZXSB";
            string Postdata = httphelper.abuse_click(postdatauri, abusepagelink + "&abuse-type=guidelines&violation-details=spam&report=Report+Abuse");
            MessageBox.Show(Postdata);
        }
        public void loginyahoo(string yahooid, string yahoopassword)
        {

            try
            {
                string yahoopge = httphelper.getHtmlfromUrl(new Uri("https://login.yahoo.com/config/login?.done=http://answers.yahoo.com%2f&.src=knowsrch&.intl=us"));
                string splitstring = ((yahoopge.Substring(yahoopge.IndexOf("onsubmit=") + 30, yahoopge.IndexOf("form: login information") - (yahoopge.IndexOf("onsubmit=") + 60))).Replace("\n", "").Replace("\t", "").Replace("<input type=\"hidden\" name=", "&").Replace(">", "").Replace("value", "").Replace("\"", "") + "&login=" + yahooid + "&passwd=" + yahoopassword + "&.save=Sign+In").Replace(" ", "").Replace("index", "");
                splitstring = splitstring.Substring(1, splitstring.Length - 1).Replace(":", "%3A").Replace("/", "%2F").Replace("knowsrch_ver=0&c=&ivt=&sg=", "knowsrch_ver%3D0%26c%3D%26ivt%3D%26sg%3D");
                string postdata = httphelper.Mainpost("https://login.yahoo.com/config/login?.done=http://answers.yahoo.com%2f&.src=knowsrch&.intl=us", splitstring);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
     
    }

}