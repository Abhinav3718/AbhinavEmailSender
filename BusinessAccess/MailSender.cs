using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;

namespace BusinessAccess
{
    public class MailSender
    {
        private readonly string _mailSubject = "";
        private readonly string _mailBody = "";
        private readonly IConfiguration _configuration;
        public MailSender(string mailSubject, string mailBody, IConfiguration configuration)
        {
            _mailBody = mailBody;
            _mailSubject = mailSubject;
            _configuration = configuration;
        }

        //to make this mail sender work without any issues, in security of google account, we need to allow low secure apps.
        public void SendMail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("domain", _configuration["EmailConfiguration:fromMailAddress"]));
            message.To.Add(new MailboxAddress("owner", _configuration["EmailConfiguration:toMailAddress"]));
            message.Subject = _mailSubject;
            message.Body = new TextPart() { Text = _mailBody };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_configuration["EmailConfiguration:smtpHost"].ToString(), int.Parse(_configuration["EmailConfiguration:smtpPort"]), false);

                smtpClient.Authenticate(_configuration["EmailConfiguration:fromMailAddress"], _configuration["EmailConfiguration:fromMailPassword"]);

                smtpClient.Send(message);
                smtpClient.Disconnect(true);
            }
        }
    }
}
