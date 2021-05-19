using EdProject.BLL.Common.Options;
using EdProject.BLL.Models.User;
using EdProject.BLL.Providers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace EdProject.BLL.EmailSender
{
    public class EmailProvider : IEmailProvider
    {
        private readonly EmailOptions _emailOptions;
        public EmailProvider(IOptions<EmailOptions> options)
        {
            _emailOptions = options.Value;
        }
        public async Task SendEmailAsync(EmailModel emailModel )
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_emailOptions.Owner, _emailOptions.Login));
            emailMessage.To.Add(new MailboxAddress(emailModel.RecipientName, emailModel.Email));
            emailMessage.Subject = emailModel.Subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailModel.Message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailOptions.SmtpHost, _emailOptions.SmtpPort);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(_emailOptions.Login, _emailOptions.Password);

                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
