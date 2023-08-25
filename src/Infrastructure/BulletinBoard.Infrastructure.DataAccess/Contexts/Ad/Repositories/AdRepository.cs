using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Contracts.Ad;
using BulletinBoard.Contracts.Attachment;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Repositories
{
    /// <inheritdoc/>
    public class AdRepository : IAdRepository
    {
        private readonly IReadOnlyCollection<AdDto> _adCollection;
        /// <summary>
        /// Инициализирует экземпляр <see cref="AdRepository"/>
        /// </summary>
        public AdRepository()
        {
            _adCollection = new List<AdDto>()
            {
                new AdDto
                {
                    Id = Guid.NewGuid(),
                    Title = "Тестовое название",
                    Description = "Тестовое описание",
                    CategoryName = "Тестовая категория",
                    Attachments = new[] { new AttachmentDto { Id = Guid.NewGuid(), Content = Array.Empty<byte>() } },
                    Price = 100,
                },
                new AdDto
                {
                    Id = Guid.NewGuid(),
                    Title = "Тестовое название 2",
                    Description = "Тестовое описание 2",
                    CategoryName = "Тестовая категория 2",
                    Attachments = new[] { new AttachmentDto { Id = Guid.NewGuid(), Content = Array.Empty<byte>() } },
                    Price = 150,
                },
                new AdDto
                {
                    Id = Guid.NewGuid(),
                    Title = "Тестовое название 3",
                    Description = "Тестовое описание 3",
                    CategoryName = "Тестовая категория 3",
                    Attachments = new[] { new AttachmentDto { Id = Guid.NewGuid(), Content = Array.Empty<byte>() } },
                    Price = 200,
                },
                new AdDto
                {
                    Id = Guid.NewGuid(),
                    Title = "Тестовое название 4",
                    Description = "Тестовое описание 4",
                    CategoryName = "Тестовая категория 4",
                    Attachments = new[] { new AttachmentDto { Id = Guid.NewGuid(), Content = Array.Empty<byte>() } },
                    Price = 250,
                },
                new AdDto
                {
                    Id = Guid.NewGuid(),
                    Title = "Тестовое название 5 ",
                    Description = "Тестовое описание 5",
                    CategoryName = "Тестовая категория 5",
                    Attachments = new[] { new AttachmentDto { Id = Guid.NewGuid(), Content = Array.Empty<byte>() } },
                    Price = 300,
                }
            };
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<AdDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            return Task.Run(() => _adCollection, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<AdDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() => _adCollection.FirstOrDefault(ad => ad.Id.Equals(id)), cancellationToken);
        }
    }
}
