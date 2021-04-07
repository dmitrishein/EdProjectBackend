using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IUserService
    {
       Task UserEdit(string userId, string userName, string firstName, string lastName);
    }
}
