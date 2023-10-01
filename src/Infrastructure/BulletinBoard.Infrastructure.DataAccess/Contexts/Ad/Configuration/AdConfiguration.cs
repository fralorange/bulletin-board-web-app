using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Configuration
{
    /// <summary>
    /// Конфигурация таблицы Ads.
    /// </summary>
    public class AdConfiguration : IEntityTypeConfiguration<Domain.Ad.Ad>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Domain.Ad.Ad> builder)
        {
            builder.ToTable(nameof(Domain.Ad.Ad));

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
