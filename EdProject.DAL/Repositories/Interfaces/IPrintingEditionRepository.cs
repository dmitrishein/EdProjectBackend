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
        IQueryable<Edition> FilterEditionList(string searchString);
    }
}
