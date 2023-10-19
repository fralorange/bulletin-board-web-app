using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Contracts.Category;

namespace BulletinBoard.Application.AppServices.Contexts.Category.Services
{
    /// <inheritdoc cref="ICategoryService"/>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализация сервиса <see cref="CategoryService"/>
        /// </summary>
        /// <param name="categoryRepository"></param>
        /// <param name="mapper"></param>
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _categoryRepository.GetAllAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Domain.Category.Category>(dto);

            return _categoryRepository.CreateAsync(category, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, UpdateCategoryDto dto, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetByPredicate(c => c.Id == id, cancellationToken).ContinueWith(t =>
            {
                var category = t.Result ?? throw new EntityNotFoundException();

                category.CategoryName = dto.CategoryName;
                category.ParentId = dto.ParentId;

                return _categoryRepository.UpdateAsync(id, category, cancellationToken);
            }).Unwrap();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetByPredicate(c => c.Id == id, cancellationToken).ContinueWith(t =>
            {
                var category = t.Result ?? throw new EntityNotFoundException();

                return _categoryRepository.DeleteAsync(category, cancellationToken);
            });
        }
    }
}
