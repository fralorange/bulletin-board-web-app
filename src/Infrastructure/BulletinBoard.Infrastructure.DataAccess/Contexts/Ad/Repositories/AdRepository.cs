using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Contracts.Ad;
using BulletinBoard.Infrastructure.Repository;
using System.Linq.Expressions;
using AdEntity = BulletinBoard.Domain.Ad.Ad;

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
        public Task<IReadOnlyCollection<AdDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var adCollection = _repository.GetAll();
            var dtoCollection = _mapper.Map<List<AdDto>>(adCollection.ToList());
            IReadOnlyCollection<AdDto> readonlyCollection = dtoCollection.AsReadOnly();

            return Task.Run(() => readonlyCollection, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<AdEntity?> GetByPredicate(Expression<Func<AdEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.Run(() => (_repository.GetAllFiltered(predicate).FirstOrDefault()), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<AdDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(id).ContinueWith(t =>
            {
                var ad = t.Result;
                return _mapper.Map<AdDto?>(ad);
            }, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, AdEntity ad, CancellationToken cancellationToken)
        {
            return _repository.UpdateAsync(ad, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(AdEntity ad, CancellationToken cancellationToken)
        {
            return _repository.DeleteAsync(ad, cancellationToken);
        }
    }
}
