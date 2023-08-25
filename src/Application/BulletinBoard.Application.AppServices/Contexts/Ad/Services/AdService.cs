using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Contracts.Ad;

namespace BulletinBoard.Application.AppServices.Contexts.Ad.Services
{
    /// <inheritdoc/>
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;

        /// <summary>
        /// Инициализирует экземпляр <see cref="AdService"/>
        /// </summary>
        /// <param name="adRepository">Репозиторий для работы с объявлениями.</param>
        public AdService(IAdRepository adRepository)
        {
            _adRepository = adRepository;
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
    }
}
