using DemoApiDotNet.Domain.InterfaceRepositories;
using DemoApiDotNet.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoApiDotNet.Infrastructure.ImplementRepositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected IDbContext _IdbContext = null;
        protected DbSet<TEntity> _dbSet;
        protected DbContext _dbContext;

        protected DbSet<TEntity> DBSet
        {
            get
            {
                if(_dbSet == null)
                {
                    _dbSet = _dbContext.Set<TEntity>() as DbSet<TEntity>;
                }

                return _dbSet;
            }

        }
        public BaseRepository(IDbContext dbContext)
        {
            _IdbContext= dbContext;
            _dbContext = (DbContext)dbContext;
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            DBSet.Add(entity);
            await _IdbContext.CommitChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> CreateAsync(List<TEntity> entities)
        {
            DBSet.AddRange(entities);
            await _IdbContext.CommitChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(long id)
        {
            var item = await DBSet.FindAsync(id);
            if(item != null)
            {
                DBSet.Remove(item);
                await _IdbContext.CommitChangesAsync();
            }
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = predicate != null ?  DBSet.Where(predicate) : DBSet;
            if(query != null)
            {
                DBSet.RemoveRange(query);
                await _IdbContext.CommitChangesAsync();
            }
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = predicate != null ? DBSet.Where(predicate) : DBSet;
            return query;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = await DBSet.SingleOrDefaultAsync(predicate);
            return query;
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            var item = await DBSet.FindAsync(id);
            return item;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _IdbContext.CommitChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(long id, TEntity entity)
        {
            var item = await DBSet.FindAsync(id);
            if(item != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _IdbContext.CommitChangesAsync();
                return entity;
            }
            return entity;
        }

        public async Task<List<TEntity>> UpdateAsync(List<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _IdbContext.CommitChangesAsync();
            }
            return entities;
        }
    }
}
