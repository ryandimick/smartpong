using System.Net;
using System.Net.Mail;

namespace SmartPong
{
    internal class NotificationEngine
    {
        private readonly string _smtpHost;
        private readonly string _smtpPassword;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;

        internal NotificationEngine(string smtpHost, int smtpPort, string smtpUsername, string smtpPassword)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }

        internal void SendEmail(string to, string subject, string body, string from = "")
        {
            using (var smtpClient = new SmtpClient(_smtpHost, _smtpPort))
            {
                var fromAddress = new MailAddress(string.IsNullOrWhiteSpace(from) ? _smtpUsername : from);
                var msg = new MailMessage { From = fromAddress, Subject = subject, Body = body };

                msg.To.Add(to);

                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);

                smtpClient.Send(msg);
                msg.Dispose();
            }
        }
    }
}
