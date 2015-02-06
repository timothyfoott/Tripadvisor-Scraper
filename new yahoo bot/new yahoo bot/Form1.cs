using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using readyahoofilesfromweb;
using internetconnectstatus;
using Globussoft.File;
using CrawlLinkToSearch;
using X509Query;
using GlobusLib;

namespace new_yahoo_bot
{
    public partial class Form1 : Form
    {
        readfilefromweb readfiles = new readfilefromweb();
        internet_connect_status internetstatus = new internet_connect_status();
        CrawlLink crawlyahoolink = new CrawlLink();
        List<string> keyword = new List<string>();
        List<string> spamlist = new List<string>();
         public Form1()
        {
            InitializeComponent();
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            if (validate_data())
            {
                return;
            }

            //keyword = readfiles.ReadSearchfile("sumit");
            //spamlist = readfiles.ReadSpamfile("sumit");
            keyword = GlobusFileHelper.ReadFiletoStringList(Application.StartupPath + "\\search.txt");
            spamlist = GlobusFileHelper.ReadFiletoStringList(Application.StartupPath + "\\spamurls.txt");
            if (!File.Exists(Application.CommonAppDataPath + "\\yahoo_Details.txt"))
            {
                string strUserDetails = txtyahooid.Text + ":" + txtyahoopassword.Text;
                GlobusFileHelper.WriteStringToTextfile(strUserDetails, Application.CommonAppDataPath + "\\yahoo_Details.txt");
            }
            foreach (string link in keyword)
            {
                List<string> templist = new List<string>();
                List<string> tempspamlist = new List<string>();
                crawlyahoolink.loginyahoo(txtyahooid.Text, txtyahoopassword.Text);
                templist = crawlyahoolink.FetchLinksToSearch(link);
                tempspamlist = crawlyahoolink.checkpagforspam(spamlist, templist);

            }
        }
        private bool validate_data()
        {
          
            if(txtyahooid.Text=="")
            {
                MessageBox.Show("Please Enter proper data","Warning");
                txtyahooid.Focus();
                return true;
              
            }else 
            if(txtyahoopassword.Text=="")
            {
                      MessageBox.Show("Please Enter proper data","Warning");
                      txtyahoopassword.Focus();
                      return true;
             
            }else
            if (txtdirectory.Text == "")
            {
                  MessageBox.Show("Please Enter proper data", "Warning");
                  txtdirectory.Focus();
                  return true;
            }
        
                return false;

        
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (internetstatus.Isinternetisconnected())
                {
                    string user_detail = readfiles.Readfiles();
                    string[] clientdetail = Regex.Split(user_detail, ":");
                    txtyahooid.Text = clientdetail[0];
                    txtyahoopassword.Text = clientdetail[1];
                    txtdirectory.Text = "sumit";
                }
                else
                {
                    MessageBox.Show("Please check your internet connection", "warning");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            crawlyahoolink.loginyahoo(txtyahooid.Text, txtyahoopassword.Text);
            crawlyahoolink.abusepageclick("http://answers.yahoo.com/answer/report;_ylt=Al_XUqVOlANIJztTYhTFFgVZxQt.;_ylv=3?qid=20100328084537AAkJjjf&kid=FbYnKzLEGUHCFnQlXN8g&.crumb=.mLJD1IobjeN", txtyahooid.Text, txtyahoopassword.Text);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
   

    }
}
