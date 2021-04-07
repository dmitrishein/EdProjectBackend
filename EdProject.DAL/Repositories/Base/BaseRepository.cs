using EdProject.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.DAL.Repositories.Base
{
    public abstract class BaseRepository<T>
    {
        private List<T> _itemsList;
        public BaseRepository()
        {

        }
        
        public void AddToRepos(T itemToAdd)
        {
            _itemsList.Add(itemToAdd);
        }
        public void DeleteFromRepos(T itemToRemove)
        {
            _itemsList.Remove(itemToRemove);
        }
 

    }
}
