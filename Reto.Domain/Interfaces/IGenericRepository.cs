using Reto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllWithInclude(params Expression<Func<T, object>>[] includes);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate); 
        Task<T> FirstOrDefaultWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> WhereWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}
