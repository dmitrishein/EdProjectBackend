using EdProject.BLL.Providers;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace EdProject.BLL.EmailSender
{
    public class EmailProvider : IEmailProvider
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Website Master", "drghost9899@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("drghost9899@gmail.com", "Dimitrius989");

                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
