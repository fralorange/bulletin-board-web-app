using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Contracts.Ad;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using AdEntity = BulletinBoard.Domain.Ad.Ad;

namespace BulletinBoard.Application.AppServices.Contexts.Ad.Services
{
    /// <inheritdoc cref="IAdService"/>
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;



        /// <summary>
        /// Инициализирует экземпляр <see cref="AdService"/>.
        /// </summary>
        /// <param name="adRepository">Репозиторий для работы с объявлениями.</param>
        /// <param name="mapper">Маппер.</param>
        /// <param name="httpContextAccessor">HttpContextAccessor.</param>
        public AdService(IAdRepository adRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _adRepository = adRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(CreateAdDto dto, CancellationToken cancellationToken)
        {
            var ad = _mapper.Map<AdEntity>(dto);
            var userId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

            ad.UserId = Guid.Parse(userId!);

            return _adRepository.CreateAsync(ad, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _adRepository.DeleteAsync(id, cancellationToken);
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
            var entityDto = GetByIdAsync(id, cancellationToken).Result 
                ?? throw new EntityNotFoundException($"Сущность с идентификатором: `{id}` не найдена!");

            var ad = _mapper.Map<AdEntity>(dto);
            ad.UserId = entityDto.User.Id;

            return _adRepository.UpdateAsync(id, ad, cancellationToken);
        }
    }
}
