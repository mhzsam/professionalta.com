using System.Net.Mail;

namespace FirstZX.Core.Senders
{

    public class SendEmail
    {
        public static void Send(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("professionalta.com");
            mail.From = new MailAddress("support@professionalta.com", "professionalta.com");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("support@professionalta.com", "QAZ123!@#");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);

        }

    }
}