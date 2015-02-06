using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
namespace GlobusLib
{
    public class GlobusHttpHelper
    {
        CookieCollection gCookies;
        HttpWebRequest gRequest;
        HttpWebResponse gResponse;
        CookieContainer mycontainer = new CookieContainer();
        public string getHtmlfromUrl(Uri url)
        {
            try
            {
            gRequest = (HttpWebRequest)WebRequest.Create(url);
            gRequest.UserAgent ="Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.7) Gecko/20091221 Firefox/3.5.7 (.NET CLR 3.5.30729)";
            gRequest.CookieContainer = new CookieContainer();
            gRequest.Method = "GET";
           
            #region CookieManagment
            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                gRequest.CookieContainer.Add(gCookies);
            }
            //Get Response for this request url
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);
                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Name;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }

            #endregion

                   StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                    string responseString = reader.ReadToEnd();
                    reader.Close();
                    return responseString;
                    
                }
                else
                {
                    return "Error";
                }
            }
            else
            {
                return "Error";
            }
            }
            catch(Exception ex)
            {
                Console.WriteLine (ex.Message);
                return "Error";

            }
        }
      
        public string Mainpost(string website, string content)
        {

            string post_data = content;
            string uri = website;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.KeepAlive = true;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.CookieContainer = new CookieContainer();
            // turn our request string into a byte stream
            byte[] postBytes = Encoding.ASCII.GetBytes(post_data);
            // this is important - make sure you specify type this way
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            
            //if (this.gCookies != null && this.gCookies.Count > 0)
            //{
            //    request.CookieContainer.Add(gCookies);
            //}
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
          
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                //check if response object has any cookies or not
                if (response.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = response.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in response.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Name;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            }
            string[] t_link = Regex.Split(response.ResponseUri.ToString(), "=");
            string str = getHtmlfromUrl(new Uri(t_link[1]));
            return str;
        }

        public string abuse_click(string website, string content)
        {
            string post_data = content;
            string uri = website;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.KeepAlive = true;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.CookieContainer = new CookieContainer();
            // turn our request string into a byte stream
            byte[] postBytes = Encoding.ASCII.GetBytes(post_data);

            // this is important - make sure you specify type this way
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                request.CookieContainer.Add(gCookies);
            }
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                //check if response object has any cookies or not
                if (response.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = response.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in response.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Name;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            }
           
            string p = response.ResponseUri.ToString();
            string str = getHtmlfromUrl(new Uri(p));
            return str;
        }
     }
}
