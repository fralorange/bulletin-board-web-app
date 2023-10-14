using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.User.Configuration
{
    /// <summary>
    /// Конфигурация отношения User.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<Domain.User.User>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Domain.User.User> builder)
        {
            builder.ToTable(nameof(Domain.User.User));

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Login).IsRequired().HasMaxLength(50);
            builder.Property(u => u.HashedPassword).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Salt).IsRequired();
            builder.Property(u => u.Role).IsRequired().HasMaxLength(25);

            builder.HasIndex(u => u.Login).IsUnique();

            builder.HasMany(u => u.Adverts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
