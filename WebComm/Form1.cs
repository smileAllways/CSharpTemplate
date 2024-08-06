using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebComm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Temp()
        {
            var req = WebRequest.Create("");
            req.Method = WebRequestMethods.Ftp.DownloadFile;
            req.Credentials = new NetworkCredential("", "");

            using (WebResponse resp = req.GetResponse())
            {
                Stream stream = resp.GetResponseStream();

                using (StreamReader sr = new StreamReader(stream))
                {
                    string data = sr.ReadToEnd();
                }
            }
        }
    }
}
