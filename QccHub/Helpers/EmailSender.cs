using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QccHub.Helpers
{
    public class EmailSender
    {
        private IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (SmtpClient smtpClient = new SmtpClient(_configuration.GetSection("AppSettings").GetSection("SmtpHost").Value,
                int.Parse(_configuration.GetSection("AppSettings").GetSection("Port").Value))
            {
                EnableSsl = bool.Parse(_configuration.GetSection("AppSettings").GetSection("EnableSsl").Value),
                UseDefaultCredentials = bool.Parse(_configuration.GetSection("AppSettings").GetSection("UseDefaultCredentials").Value),
                Credentials = new NetworkCredential(_configuration.GetSection("AppSettings").GetSection("UserName").Value,
               _configuration.GetSection("AppSettings").GetSection("Password").Value)
            })
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_configuration.GetSection("AppSettings").GetSection("FromAddress").Value);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = message;
                mailMessage.To.Add(email);
                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }
    }
}    