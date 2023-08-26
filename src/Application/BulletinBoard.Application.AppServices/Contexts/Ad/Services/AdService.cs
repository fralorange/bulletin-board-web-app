using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Contracts.Ad;

using AdEntity = BulletinBoard.Domain.Ad.Ad;

namespace BulletinBoard.Application.AppServices.Contexts.Ad.Services
{
    /// <inheritdoc/>
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует экземпляр <see cref="AdService"/>
        /// </summary>
        /// <param name="adRepository">Репозиторий для работы с объявлениями.</param>
        /// <param name="mapper">Маппер</param>
        public AdService(IAdRepository adRepository, IMapper mapper)
        {
            _adRepository = adRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(CreateAdDto dto, CancellationToken cancellationToken)
        {
            return _adRepository.CreateAsync(_mapper.Map<AdEntity>(dto), cancellationToken);
        }

        /// <inheritdoc/> 
        public Task<IReadOnlyCollection<AdDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            return _adRepository.GetAllAsync(cancellationToken, pageSize, pageIndex);
        }

        /// <inheritdoc/>
        public Task<AdDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _adRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, UpdateAdDto dto, CancellationToken cancellationToken)
        {
            return _adRepository.UpdateAsync(id, _mapper.Map<AdEntity>(dto), cancellationToken);
        }
    }
}
