using EdProject.BLL.Models.User;
using EdProject.BLL.Providers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

namespace EdProject.BLL.EmailSender
{
    public class EmailProvider : IEmailProvider
    {
        IConfiguration _config;
        public EmailProvider(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task SendEmailAsync(EmailModel emailModel )
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_config["EmailProvider:Owner"], _config["EmailProvider:Login"]));
            emailMessage.To.Add(new MailboxAddress(emailModel.RecipientName, emailModel.Email));
            emailMessage.Subject = emailModel.Subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailModel.Message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_config["EmailProvider:SmtpHost"], int.Parse(_config["EmailProvider:SmtpPort"]));

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(_config["EmailProvider:Login"], _config["EmailProvider:Password"]);

                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
