using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Infrastructure.Repository;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Attachment.Repositories
{
    /// <inheritdoc cref="IAttachmentRepository"/>
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly IRepository<Domain.Attachment.Attachment> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует репозиторий изображений.
        /// </summary>
        /// <param name="repository">Репозиторий.</param>

        public AttachmentRepository(IRepository<Domain.Attachment.Attachment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<AttachmentDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var attCollection = _repository.GetAll();
            var dtoCollection = _mapper.Map<List<AttachmentDto>>(attCollection.ToList());
            IReadOnlyCollection<AttachmentDto> readonlyCollection = dtoCollection.AsReadOnly();

            return Task.Run(() => readonlyCollection, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<AttachmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var att = _repository.GetByIdAsync(id).Result;
            return Task.Run(() => _mapper.Map<AttachmentDto?>(att));
        }

        /// <inheritdoc/>
        public Task<Domain.Attachment.Attachment?> GetByPredicate(Expression<Func<Domain.Attachment.Attachment, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.Run(() => (_repository.GetAllFiltered(predicate).FirstOrDefault()), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(Domain.Attachment.Attachment attachment, CancellationToken cancellationToken)
        {
            _repository.AddAsync(attachment, cancellationToken);
            return Task.Run(() => attachment.Id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Domain.Attachment.Attachment attachment, CancellationToken cancellationToken)
        {
            return _repository.DeleteAsync(attachment, cancellationToken);
        }
    }
}
