using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoApiDotNet.Domain.InterfaceRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(long id);
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate); 
        Task<TEntity> CreateAsync(TEntity entity);
        Task<List<TEntity>> CreateAsync(List<TEntity> entities);
        Task DeleteAsync(long id);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(long id, TEntity entity);
        Task<List<TEntity>> UpdateAsync(List<TEntity> entities);
    }
}
