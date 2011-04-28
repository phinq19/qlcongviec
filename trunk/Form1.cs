using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace NewProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            foreach (System.Windows.Forms.Control ctr in htmlEditor1.Controls)
            {
                if (ctr.Name == "lnkLabelPurchaseLink")
                {
                    ctr.Visible = false;
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string str = htmlEditor1.BodyHtml;
            //MessageBox.Show(str);
            string _mailServer = "smtp.gmail.com";
             int _mailPort = 465; // Work well, 465 Timeout

             string _user = "tranthanhson1986@gmail.com";
             string _pass = "264286313";

             string _mailFrom = "tranthanhson1986@gmail.com";
             string _mailTo = "sonituns@gmail.com";
             MailMessage mailMessage = new MailMessage();
             SmtpClient mailClient = new SmtpClient(_mailServer, _mailPort);
             mailClient.Timeout = 150000;
             mailClient.Credentials = new NetworkCredential(_user, _pass);
             mailClient.EnableSsl = true;
             //mailClient.UseDefaultCredentials = true; // no work

             mailMessage.IsBodyHtml = true;
             mailMessage.From = new MailAddress(_mailFrom);
             mailMessage.Subject = "Hello APhuong test send mail via Gmail SMTP";
             mailMessage.Body = str;
             mailMessage.To.Add(_mailTo);
             try
             {
                     mailClient.Send(mailMessage);
                     MessageBox.Show("Send mail success");
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Send mail fail, " + ex.Message);
             }
        }
    }
}
