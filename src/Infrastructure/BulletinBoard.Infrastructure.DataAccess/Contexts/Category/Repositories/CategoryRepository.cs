using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Contracts.Ad;
using BulletinBoard.Contracts.Category;
using BulletinBoard.Infrastructure.Repository;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Category.Repositories
{
    /// <inheritdoc cref="ICategoryRepository"/>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository<Domain.Category.Category> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует репозиторий <see cref="CategoryRepository"/>
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public CategoryRepository(IRepository<Domain.Category.Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var categoryCollection = _repository.GetAll();
            var dtoCollection = _mapper.Map<List<CategoryDto>>(categoryCollection.ToList());
            IReadOnlyCollection<CategoryDto> readonlyCollection = dtoCollection.AsReadOnly();

            return Task.Run(() => readonlyCollection);
        }

        /// <inheritdoc/>
        public Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(id).ContinueWith(t =>
            {
                var category = t.Result;
                return _mapper.Map<CategoryDto?>(category);
            }, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Domain.Category.Category?> GetByPredicate(Expression<Func<Domain.Category.Category, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.Run(() => (_repository.GetAllFiltered(predicate).FirstOrDefault()), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(Domain.Category.Category category, CancellationToken cancellationToken)
        {
            _repository.AddAsync(category, cancellationToken);
            return Task.FromResult(category.Id);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, Domain.Category.Category category, CancellationToken cancellationToken)
        {
            return _repository.UpdateAsync(category, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Domain.Category.Category category, CancellationToken cancellationToken)
        {
            return _repository.DeleteAsync(category, cancellationToken);
        }
    }
}
