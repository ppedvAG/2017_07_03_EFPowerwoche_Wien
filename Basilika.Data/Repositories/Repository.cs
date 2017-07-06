using Basilika.Core.Models;
using Basilika.Core.Repositories;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace Basilika.Data.Repositories
{
    public abstract class Repository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : Entity
    {
        protected TContext Context { get; }
        protected DbSet<TEntity> Entries { get; }

        public Repository(TContext context)
        {
            Context = context;
            Entries = context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return Entries.Find(id);
        }
        public Task<TEntity> GetAsync(int id)
        {
            return Entries.FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entries.ToList();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Entries.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllOrderdDescendigByAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return await Entries.OrderByDescending(keySelector).ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entries.Where(predicate).ToList();
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entries.Where(predicate).ToListAsync();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entries.FirstOrDefault(predicate);
        }
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Entries.FirstOrDefaultAsync(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entries.SingleOrDefault(predicate);
        }
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Entries.SingleOrDefaultAsync(predicate);
        }

        public void Add(TEntity entity)
        {
            Entries.Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entries.AddRange(entities);
        }
        public void Remove(TEntity entity)
        {
            Entries.Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entries.RemoveRange(entities);
        }
    }
}
