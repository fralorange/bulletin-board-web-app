using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.Repository
{
    /// <inheritdoc cref="IRepository{TEntity}"/>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            return DbSet.Where(predicate);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task AddAsync(TEntity model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            await DbSet.AddAsync(model, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(TEntity model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            DbSet.Update(model);
            await SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var model = DbSet.FindAsync(id).Result;

            if (model == null)
            {
                throw new ArgumentException(nameof(model));
            }

            DbSet.Remove(model);
            await SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SaveChangesAsync(CancellationToken cancellationToken=default)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
