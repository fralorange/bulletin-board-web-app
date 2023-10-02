using BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Configuration;
using BulletinBoard.Infrastructure.DataAccess.Contexts.User.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Infrastructure.DataAccess
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    public class BaseDbContext : DbContext
    {
        /// <inheritdoc cref="DbContext"/>
        public BaseDbContext(DbContextOptions options) : base(options) { }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
