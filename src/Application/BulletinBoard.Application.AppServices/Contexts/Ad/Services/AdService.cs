using AutoMapper;
using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Authentication.Services;
using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Application.AppServices.Filtration.Ad.Specification;
using BulletinBoard.Application.AppServices.Pagination.Helpers;
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
        private readonly IEntityAuthorizationService _entityAuthorizationService;

        /// <summary>
        /// Инициализирует экземпляр <see cref="AdService"/>.
        /// </summary>
        /// <param name="adRepository">Репозиторий для работы с объявлениями.</param>
        /// <param name="mapper">Маппер.</param>
        /// <param name="httpContextAccessor">HttpContextAccessor.</param>
        /// <param name="entityAuthorizationService"></param>
        public AdService(IAdRepository adRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IEntityAuthorizationService entityAuthorizationService)
        {
            _adRepository = adRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _entityAuthorizationService = entityAuthorizationService;
        }

        /// <inheritdoc/> 
        public Task<IReadOnlyCollection<AdDto>> GetAllAsync(AdSpecification specification, int pageSize, int pageIndex, CancellationToken cancellationToken)
        {
            var modelCollection = _adRepository.GetAllAsync(specification, cancellationToken);
            var paginatedCollection = PaginationHelper<AdDto>.SplitByPages(modelCollection, pageSize, pageIndex);
            
            return paginatedCollection;
        }

        /// <inheritdoc/>
        public Task<AdDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _adRepository.GetByIdAsync(id, cancellationToken);
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
        public Task UpdateAsync(Guid id, UpdateAdDto dto, CancellationToken cancellationToken)
        {
            return _adRepository.GetByPredicate(a => a.Id == id, cancellationToken).ContinueWith(t => {
                var ad = t.Result ?? throw new EntityNotFoundException();

                return _entityAuthorizationService.Validate(_httpContextAccessor.HttpContext!.User, id, AuthRoles.Admin).ContinueWith(t2 =>
                {
                    if (t2.Result)
                        throw new EntityForbiddenException();

                    ad.Title = dto.Title;
                    ad.Description = dto.Description;
                    ad.Price = dto.Price;

                    return _adRepository.UpdateAsync(id, ad, cancellationToken);
                });
            });
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _adRepository.GetByPredicate(a => a.Id == id, cancellationToken).ContinueWith(t => {
                var ad = t.Result ?? throw new EntityNotFoundException();

                return _entityAuthorizationService.Validate(_httpContextAccessor.HttpContext!.User, id, AuthRoles.Admin).ContinueWith(t2 =>
                {
                    if (t2.Result)
                        throw new EntityForbiddenException();

                    return _adRepository.DeleteAsync(ad, cancellationToken);
                });
            }).Unwrap();
        }
    }
}
