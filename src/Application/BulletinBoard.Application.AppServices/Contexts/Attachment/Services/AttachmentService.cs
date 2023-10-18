using AutoMapper;
using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Authentication.Services;
using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Contracts.Attachment;
using Microsoft.AspNetCore.Http;

namespace BulletinBoard.Application.AppServices.Contexts.Attachment.Services
{
    /// <inheritdoc cref="IAttachmentService"/>
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEntityAuthorizationService _entityAuthorizationService;

        /// <summary>
        /// Инициализирует <see cref="AttachmentService"/>
        /// </summary>
        /// <param name="attachmentRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="entityAuthorizationService"></param>
        public AttachmentService(IAttachmentRepository attachmentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IEntityAuthorizationService entityAuthorizationService)
        {
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _entityAuthorizationService = entityAuthorizationService;
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<AttachmentDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _attachmentRepository.GetAllAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task<AttachmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _attachmentRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(CreateAttachmentDto dto, CancellationToken cancellationToken)
        {
            var attachment = _mapper.Map<Domain.Attachment.Attachment>(dto);

            return _entityAuthorizationService.Validate(_httpContextAccessor.HttpContext!.User, dto.AdId, AuthRoles.Admin).ContinueWith(t =>
            {
                if (t.Result)
                    throw new EntityForbiddenException();
                return _attachmentRepository.CreateAsync(attachment, cancellationToken);
            }).Unwrap();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var att = _attachmentRepository.GetByPredicate(a => a.Id == id, cancellationToken).Result ?? throw new EntityNotFoundException();

            return _entityAuthorizationService.Validate(_httpContextAccessor.HttpContext!.User, att.AdId, AuthRoles.Admin).ContinueWith(t =>
            {
                if (t.Result)
                    throw new EntityForbiddenException();
                return _attachmentRepository.DeleteAsync(att, cancellationToken);
            }).Unwrap();
        }
    }
}
