using Microsoft.EntityFrameworkCore;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QccHub.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public virtual Task<List<T>> GetAllAsync()
        {
            return context.Set<T>().ToListAsync();
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            return context.Set<T>().FirstOrDefaultAsync(x => x.ID == id);
        }


        public T Add(T entity)
        {
            context.Set<T>().Add(entity);
            return entity;
        }
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
    }
}
