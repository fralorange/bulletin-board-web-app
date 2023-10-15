using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Comment.Repositories;
using BulletinBoard.Contracts.Comment;
using BulletinBoard.Domain.Ad;
using BulletinBoard.Infrastructure.Repository;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Comment.Repositories
{
    /// <inheritdoc cref="ICommentRepository"/>
    public class CommentRepository : ICommentRepository
    {
        private readonly IRepository<Domain.Comment.Comment> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализация репозитория <see cref="CommentRepository"/>
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public CommentRepository(IRepository<Domain.Comment.Comment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<CommentDto>> GetAllAsync(CancellationToken cancellationToken, int limit = 10)
        {
            var commentCollection = _repository.GetAll();
            var dtoCollection = _mapper.Map<List<CommentDto>>(commentCollection.ToList());
            IReadOnlyCollection<CommentDto> readonlyCollection = dtoCollection.AsReadOnly();

            return Task.Run(() => readonlyCollection);
        }

        /// <inheritdoc/>
        public Task<CommentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var comment = _repository.GetByIdAsync(id).Result;
            return Task.Run(() => _mapper.Map<CommentDto?>(comment), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Domain.Comment.Comment?> GetByPredicate(Expression<Func<Domain.Comment.Comment, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.Run(() => (_repository.GetAllFiltered(predicate).FirstOrDefault()), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(Domain.Comment.Comment comment, CancellationToken cancellationToken)
        {
            _repository.AddAsync(comment, cancellationToken);
            return Task.FromResult(comment.Id);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, Domain.Comment.Comment comment, CancellationToken cancellationToken)
        {
            return _repository.UpdateAsync(comment, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Domain.Comment.Comment comment, CancellationToken cancellationToken)
        {
            return _repository.DeleteAsync(comment, cancellationToken);
        }
    }
}
