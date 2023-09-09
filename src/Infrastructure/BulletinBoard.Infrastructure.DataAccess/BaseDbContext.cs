using BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Infrastructure.DataAccess
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    public class BaseDbContext : DbContext
    {
        /// <inheritdoc/>
        public BaseDbContext(DbContextOptions options) : base(options) { }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
