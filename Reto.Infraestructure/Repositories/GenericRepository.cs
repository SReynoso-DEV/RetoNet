using Microsoft.EntityFrameworkCore;
using Reto.Domain.Interfaces;
using Reto.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Infraestructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly RetoContext _context;

        public GenericRepository(RetoContext context)
        {
            _context = context;
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> FirstOrDefaultWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllWithInclude(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> WhereWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query.Where(predicate);
        }
    }
}
