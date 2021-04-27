using EdProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IBaseRepository<Edition>
    {
        public Task<List<Edition>> FilterEditionList(string searchString);
        public Task<List<Edition>> Pagination(int pageNumber, int pageSize);
    }
}
