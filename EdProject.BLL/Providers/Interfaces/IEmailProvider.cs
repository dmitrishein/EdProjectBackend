using EdProject.BLL.Models.User;
using System.Threading.Tasks;

namespace EdProject.BLL.Providers
{
    public interface IEmailProvider
    {
        Task SendEmailAsync(EmailModel emailModel);
    }
}
