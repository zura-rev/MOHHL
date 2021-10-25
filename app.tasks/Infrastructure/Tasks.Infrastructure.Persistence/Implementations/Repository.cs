using Tasks.Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Infrastructure.Persistence.Implementations
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext context;
        public Repository(DataContext context) => this.context = context;

        public virtual void Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public virtual TEntity Read(int id)
        {
            return context.Set<TEntity>().Find(id);
        }
        public virtual async Task<TEntity> ReadAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        public virtual IEnumerable<TEntity> Read()
        {
            return context.Set<TEntity>();
        }
        public virtual async Task<IEnumerable<TEntity>> ReadAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }
        public virtual IEnumerable<TEntity> Read(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }
        public virtual async Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }
        public virtual void Update(int id, TEntity entity)
        {
            var existing = context.Set<TEntity>().Find(id);
            this.context.Entry(existing).CurrentValues.SetValues(entity);
        }


        public virtual void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }
        public virtual void Delete(int id)
        {
            context.Set<TEntity>().Remove(this.Read(id));
        }


        public virtual bool Check(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Any(predicate);
        }
        public virtual async Task<bool> CheckAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().AnyAsync(predicate);
        }


        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Count(predicate);
        }
    }
}
