using AutoMapper;
using ForumWebsite.Dtos.Comments;
using ForumWebsite.Models;
using ForumWebsite.Repositories.Comments;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ForumWebsite.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentService (ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<CommentDto> CreateComment(CreateCommentDto dto, int userId)
        {
            var comment = _mapper.Map<Comment>(dto);
            comment.UserId = userId;
            comment.CreatedAt = DateTime.UtcNow;
            await _commentRepository.AddComment(comment);
            return _mapper.Map<CommentDto>(comment)!;
        }

        public async Task DeleteComment(int commentId, int userId)
        {
            var comment = await _commentRepository.GetCommentById(commentId) 
                ?? throw new KeyNotFoundException("Comment not found");

            if (comment.UserId != userId)
                throw new UnauthorizedAccessException("You are not allowed to delete this comment");

            await _commentRepository.DeleteComment(commentId);
        }

        public async Task<CommentDto> GetCommentById(int commentId)
        {
            var comment = await _commentRepository.GetCommentById(commentId);
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<ICollection<CommentDto>> GetCommentsByThreadId(int threadId)
        {
            var commentThreads = await _commentRepository.GetCommentsByThreadId(threadId);
            return _mapper.Map<ICollection<CommentDto>>(commentThreads);
        }

        public Task<(int upvotes, int downvotes)> GetVoteSummary(int commentId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateComment(int commentId, UpdateCommentDto dto, int userId)
        {
            var comment = await _commentRepository.GetCommentById(commentId)
                ?? throw new KeyNotFoundException("key is null");
            if (comment.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this comment.");
            }

            comment.Content = dto.Content;

            await _commentRepository.UpdateComment(comment);
        }
    }
}
