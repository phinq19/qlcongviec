using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace NewProject
{
    public class SendMail
    {
        public SmtpClient mailClient;
        public SendMail(string strSmtpClient, int port, string sendEmailsFrom, string sendEmailsFromPassword, bool EnableSsl, int Timeout)
        {
            NetworkCredential cred = new NetworkCredential(sendEmailsFrom, sendEmailsFromPassword);
            mailClient = new SmtpClient(strSmtpClient, port);
            mailClient.EnableSsl = EnableSsl;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Timeout = Timeout;
            mailClient.Credentials = cred;
            
        }
        public bool BeginSendMail( string sendEmailsFrom, string sendEmailsFromDislay, string sendEmailsTo,string Subject,string Body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(sendEmailsTo);
                mailMessage.Subject = Subject;
                mailMessage.Body = Body;
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new MailAddress(sendEmailsFrom, sendEmailsFromDislay);
                mailClient.Send(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
