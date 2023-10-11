using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Contracts.Ad;
using BulletinBoard.Infrastructure.Repository;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AdEntity = BulletinBoard.Domain.Ad.Ad;
using AttachmentEntity = BulletinBoard.Domain.Attachment.Attachment;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Repositories
{
    /// <inheritdoc cref="IAdRepository"/>
    public class AdRepository : IAdRepository
    {
        private readonly IRepository<AdEntity> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует репозиторий объявлений.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        /// <param name="mapper">Маппер.</param>
        public AdRepository(IRepository<AdEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(AdEntity ad, CancellationToken cancellationToken)
        {
            _repository.AddAsync(ad, cancellationToken);
            return Task.FromResult(ad.Id);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<AdDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            var adCollection = _repository.GetAll();
            var dtoCollection = _mapper.Map<List<AdDto>>(adCollection.ToList());
            IReadOnlyCollection<AdDto> readonlyCollection = dtoCollection.AsReadOnly();

            return Task.Run(() => readonlyCollection, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Domain.Ad.Ad?> GetByPredicate(Expression<Func<AdEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.Run(() => (_repository.GetAllFiltered(predicate).FirstOrDefault()), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<AdDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var ad = _repository.GetByIdAsync(id).Result;
            return Task.Run(() => _mapper.Map<AdDto?>(ad), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var model = _repository.GetByIdAsync(id).Result;
            if (model == null)
                return Task.FromResult(false);
            _repository.DeleteAsync(model, cancellationToken);
            return Task.FromResult(true);
        }


        /// <inheritdoc/>
        public Task<bool> UpdateAsync(Guid id, AdEntity ad, CancellationToken cancellationToken)
        {
            _repository.UpdateAsync(ad, cancellationToken);
            return Task.FromResult(true);
        }
    }
}
