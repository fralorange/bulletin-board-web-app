using BulletinBoard.Infrastructure.DataAccess.Interfaces;

namespace BulletinBoard.Infrastructure.DataAccess
{
    /// <inheritdoc cref="IDbInitializer"/>
    public class EfDbInitializer : IDbInitializer
    {
        private readonly BaseDbContext _dbContext;

        /// <summary>
        /// Конструктор инициализатора БД.
        /// </summary>
        /// <param name="dbContext">Контекст БД.</param>
        public EfDbInitializer(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public void InitializeDb()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }
    }
}
