using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Category.Configuration
{
    /// <summary>
    /// Конфигурация отношения Category.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Domain.Category.Category>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Domain.Category.Category> builder)
        {
            builder.ToTable(nameof(Domain.Category.Category));

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(30);

            builder.HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Adverts)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
