using BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Configuration;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Attachment.Configuration;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Category.Configuration;
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
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
