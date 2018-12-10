using OttooDo.Model.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Interface.Repository.Base
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> FindAsync(string Id);
        IQueryable<T> GetQueryable();
        Task<bool> DeleteAsync(T entity);
        Task InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
