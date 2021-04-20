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
        public Task<IEnumerable<Edition>> FilterEditionList(string searchString);
        public Task<IEnumerable<Edition>> Paging(int pageNumber, int pageSize);
    }
}
