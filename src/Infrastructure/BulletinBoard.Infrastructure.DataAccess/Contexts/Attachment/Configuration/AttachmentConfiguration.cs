using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Attachment.Configuration
{
    /// <summary>
    /// Конфигурация отношения Attachment.
    /// </summary>
    public class AttachmentConfiguration : IEntityTypeConfiguration<Domain.Attachment.Attachment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Domain.Attachment.Attachment> builder)
        {
            builder.ToTable(nameof(Domain.Attachment.Attachment));

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Content).IsRequired();

            builder.HasOne(a => a.Ad)
                .WithMany(ad => ad.Attachments)
                .HasForeignKey(a => a.AdId);
        }
    }
}
