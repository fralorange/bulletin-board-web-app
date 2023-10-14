using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Configuration
{
    /// <summary>
    /// Конфигурация отношения Ad.
    /// </summary>
    public class AdConfiguration : IEntityTypeConfiguration<Domain.Ad.Ad>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Domain.Ad.Ad> builder)
        {
            builder.ToTable(nameof(Domain.Ad.Ad));

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Title).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Description).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Price).IsRequired();
            builder.Property(a => a.UserId).IsRequired();

            builder.HasOne(a => a.User)
                .WithMany(u => u.Adverts)
                .HasForeignKey(a => a.UserId);

            builder.HasMany(a => a.Attachments)
                .WithOne(att => att.Ad)
                .HasForeignKey(att => att.AdId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
