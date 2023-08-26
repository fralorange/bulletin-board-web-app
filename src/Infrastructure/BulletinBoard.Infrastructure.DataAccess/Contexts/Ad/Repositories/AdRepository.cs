using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Contracts.Ad;
using System.Collections.ObjectModel;

using AdEntity = BulletinBoard.Domain.Ad.Ad;
using AttachmentEntity = BulletinBoard.Domain.Attachment.Attachment;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Repositories
{
    /// <inheritdoc/>
    public class AdRepository : IAdRepository
    {
        private readonly List<AdEntity> _adCollection;
        private readonly IMapper _mapper;
        /// <summary>
        /// Инициализирует экземпляр <see cref="AdRepository"/>
        /// </summary>
        public AdRepository(IMapper mapper)
        {
            _adCollection = new List<AdEntity>()
            {
                new AdEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Тестовое название",
                    Description = "Тестовое описание",
                    CategoryId = Guid.NewGuid(),
                    Attachments = new[] { new AttachmentEntity { Id = Guid.NewGuid(), Content = new byte[] { 111, 222, 11, 254} } },
                    Price = 100,
                },
                new AdEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Тестовое название 2",
                    Description = "Тестовое описание 2",
                    CategoryId = Guid.NewGuid(),
                    Attachments = new[] { new AttachmentEntity { Id = Guid.NewGuid(), Content = Array.Empty<byte>() } },
                    Price = 150,
                },
                new AdEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Тестовое название 3",
                    Description = "Тестовое описание 3",
                    CategoryId = Guid.NewGuid(),
                    Attachments = new[] { new AttachmentEntity { Id = Guid.NewGuid(), Content = Array.Empty<byte>() } },
                    Price = 200,
                },
                new AdEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Тестовое название 4",
                    Description = "Тестовое описание 4",
                    CategoryId = Guid.NewGuid(),
                    Attachments = new[] { new AttachmentEntity { Id = Guid.NewGuid(), Content = Array.Empty<byte>() } },
                    Price = 250,
                },
                new AdEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Тестовое название 5 ",
                    Description = "Тестовое описание 5",
                    CategoryId = Guid.NewGuid(),
                    Attachments = new[] { new AttachmentEntity { Id = Guid.NewGuid(), Content = Array.Empty<byte>() } },
                    Price = 300,
                }
            };

            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(AdEntity ad, CancellationToken cancellationToken)
        {
            ad.Id = Guid.NewGuid();
            _adCollection.Add(ad);
            return Task.Run(() => ad.Id);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<AdDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            var dtoCollection = _adCollection.Select(_mapper.Map<AdDto>).ToList().AsReadOnly();
            return Task.Run(() => dtoCollection, cancellationToken).ContinueWith(x => new ReadOnlyCollection<AdDto>(x.Result) as IReadOnlyCollection<AdDto>);
        }

        /// <inheritdoc/>
        public Task<AdDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() => _mapper.Map<AdDto?>(_adCollection.FirstOrDefault(ad => ad.Id.Equals(id))), cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, AdEntity ad, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                int index = _adCollection.FindIndex(entity => entity.Id == id);
                ad.Id = id;
                _adCollection[index] = ad;
            }, cancellationToken);
        }
    }
}
