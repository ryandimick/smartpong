using System.Net;
using System.Net.Mail;

namespace SmaPong.Business
{
    public static class NotificationBusiness
    {
        public static void Send(string to, string subject, string body)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                var fromAddress = new MailAddress("alerts@smaascern.com");
                var msg = new MailMessage { From = fromAddress, Subject = subject, Body = body };

                msg.To.Add(to);

                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("alerts@smaascern.com", "0pC0nxp$!!");

                smtpClient.Send(msg);
                msg.Dispose();
            }
        }
    }
}