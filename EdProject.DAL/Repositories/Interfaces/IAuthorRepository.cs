using EdProject.DAL.Entities;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public Task RemoveAuthorById(long id);
    }
}
