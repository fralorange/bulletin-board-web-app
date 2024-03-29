﻿using AutoMapper;
using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Authentication.Services;
using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Comment.Repositories;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Application.AppServices.Filtration.Comment.Specification;
using BulletinBoard.Application.AppServices.Pagination.Helpers;
using BulletinBoard.Contracts.Comment;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BulletinBoard.Application.AppServices.Contexts.Comment.Services
{
    /// <inheritdoc cref="ICommentService"/>
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEntityAuthorizationService _entityAuthorizationService;

        /// <summary>
        /// Инициализация сервиса <see cref="CommentService"/>
        /// </summary>
        /// <param name="commentRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="entityAuthorizationService"></param>
        public CommentService(ICommentRepository commentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IEntityAuthorizationService entityAuthorizationService)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _entityAuthorizationService = entityAuthorizationService;
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<CommentDto>> GetAllAsync(CommentSpecification specification, int pageSize, int pageIndex, CancellationToken cancellationToken)
        {
            var modelCollection = _commentRepository.GetAllAsync(specification, cancellationToken);
            var paginatedCollection = PaginationHelper<CommentDto>.SplitByPages(modelCollection, pageSize, pageIndex);

            return paginatedCollection;
        }

        /// <inheritdoc/>
        public Task<CommentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _commentRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(CreateCommentDto dto, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Domain.Comment.Comment>(dto);

            comment.UserId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return _commentRepository.CreateAsync(comment, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, UpdateCommentDto dto, CancellationToken cancellationToken)
        {
            return _commentRepository.GetByPredicate(c => c.Id == id, cancellationToken).ContinueWith(t => {
                var comment = t.Result ?? throw new EntityNotFoundException();

                return _entityAuthorizationService.ValidateUserOnly(_httpContextAccessor.HttpContext!.User, comment.UserId, AuthRoles.Admin).ContinueWith(t2 =>
                {
                    if (t2.Result)
                        throw new EntityForbiddenException();

                    comment.Content = dto.Content;
                    comment.Rating = dto.Rating;

                    return _commentRepository.UpdateAsync(id, comment, cancellationToken);
                }).Unwrap();
            }).Unwrap();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _commentRepository.GetByPredicate(c => c.Id == id, cancellationToken).ContinueWith(t => {
                var comment = t.Result ?? throw new EntityNotFoundException();

                return _entityAuthorizationService.ValidateUserOnly(_httpContextAccessor.HttpContext!.User, comment.UserId, AuthRoles.Admin).ContinueWith(t2 =>
                {
                    if (t2.Result)
                        throw new EntityForbiddenException();

                    return _commentRepository.DeleteAsync(comment, cancellationToken);
                }).Unwrap();
            }).Unwrap();
        }
    }
}
