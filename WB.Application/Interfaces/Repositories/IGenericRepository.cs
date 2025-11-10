using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WB.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds an entity to the DbContext.
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets and entity from DB by its id.
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="includeProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetById(int entityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all table records
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> Get(
                Expression<Func<TEntity, bool>>? filter = null,
                string[]? includeProperties = default,
                CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the number of records.
        /// </summary>
        /// <returns>int</returns>
        Task<int> Count();

        /// <summary>
        /// Deletes an entity by its id.
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteById(int entityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
    }
}
