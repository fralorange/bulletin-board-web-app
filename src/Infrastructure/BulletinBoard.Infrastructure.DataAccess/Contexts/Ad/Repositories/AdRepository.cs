using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Contracts.Ad;
using BulletinBoard.Infrastructure.Repository;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using AdEntity = BulletinBoard.Domain.Ad.Ad;
using AttachmentEntity = BulletinBoard.Domain.Attachment.Attachment;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Repositories
{
    /// <inheritdoc cref="IAdRepository"/>
    public class AdRepository : IAdRepository
    {
        private readonly IRepository<AdEntity> _repository;
        private readonly IMapper _mapper;

        /// <inheritdoc/>
        public AdRepository(IRepository<AdEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(AdEntity ad, CancellationToken cancellationToken)
        {
            _repository.AddAsync(ad, cancellationToken);
            return Task.Run(() => ad.Id);
        }

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<AdDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            var dtoCollection = _repository.GetAll().Select(_mapper.Map<AdDto>).ToList().AsReadOnly();
            return Task.Run(() => dtoCollection, cancellationToken).ContinueWith(x => new ReadOnlyCollection<AdDto>(x.Result) as IReadOnlyCollection<AdDto>);
        }

        /// <inheritdoc/>
        public Task<AdDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() => _mapper.Map<AdDto?>(_repository.GetByIdAsync(id)), cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, AdEntity ad, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
