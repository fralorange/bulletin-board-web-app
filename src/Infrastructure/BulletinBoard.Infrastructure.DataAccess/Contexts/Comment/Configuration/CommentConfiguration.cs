using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Comment.Configuration
{
    /// <summary>
    /// Конфигурация отношения Comment.
    /// </summary>
    public class CommentConfiguration : IEntityTypeConfiguration<Domain.Comment.Comment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Domain.Comment.Comment> builder)
        {
            builder.ToTable(nameof(Domain.Comment.Comment));

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Content).IsRequired().HasMaxLength(1000);
            builder.Property(c => c.Rating).IsRequired();
            builder.Property(c => c.PublishedAt).IsRequired().HasDefaultValueSql("now()");

            builder.HasOne(c => c.Ad)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AdId);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);
        }
    }
}
