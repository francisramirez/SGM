

using SGM.Domain.Base;
using System.Linq.Expressions;

namespace SGM.Application.Contracts.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
      
        Task<OperationResult> GetByIdAsync(int id);
        Task<OperationResult> AddAsync(TEntity entity);
        Task<OperationResult> UpdateAsync(TEntity entity);
        Task<OperationResult> DeleteAsync(TEntity entity);
       Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);

    }
}
