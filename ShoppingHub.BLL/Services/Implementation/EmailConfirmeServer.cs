using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using ShoppingHub.BLL.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.Services.Implementation
{
    public class EmailConfirmeServer : IEmailConfirmeServer 
    {
        private readonly IConfiguration _configuration;
        public EmailConfirmeServer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fMail = _configuration["Smtp:Username"];
            var fPassword = _configuration["Smtp:Password"];
            var theMsg = new MailMessage();
            theMsg.Subject = subject;
            theMsg.From = new MailAddress(fMail);
            theMsg.To.Add(email);
            theMsg.Body = $"<html><body>{htmlMessage}</body></html>";
            theMsg.IsBodyHtml = true;

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fMail, fPassword),
                Port = 587
            };
            smtp.Send(theMsg);
        }
    }
}
