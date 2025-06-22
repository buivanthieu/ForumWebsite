using AutoMapper;
using ForumWebsite.Dtos.Comments;
using ForumWebsite.Models;
using ForumWebsite.Repositories.Comments;
using ForumWebsite.Repositories.Users;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ForumWebsite.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public CommentService (ICommentRepository commentRepository, IMapper mapper, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<CommentDto> CreateComment(CreateCommentDto dto, int userId)
        {
            var comment = _mapper.Map<Comment>(dto);
            comment.UserId = userId;
            comment.CreatedAt = DateTime.UtcNow;
            await _commentRepository.AddComment(comment);

            var user = await _userRepository.GetUserById(userId);
            user.TotalComments += 1;
            await _userRepository.UpdateUser(user);
            return _mapper.Map<CommentDto>(comment)!;
        }

        public async Task DeleteComment(int commentId, int userId)
        {
            var comment = await _commentRepository.GetCommentById(commentId) 
                ?? throw new KeyNotFoundException("Comment not found");

            if (comment.UserId != userId)
                throw new UnauthorizedAccessException("You are not allowed to delete this comment");

            await _commentRepository.DeleteComment(commentId);

            var user = await _userRepository.GetUserById(userId);
            user.TotalComments -= 1;
            await _userRepository.UpdateUser(user);
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

        public async Task<CommentDto> ReplyComment(ReplyCommentDto replyCommentDto, int userId, int parentCommentId)
        {
            var parentComment = await _commentRepository.GetCommentById(parentCommentId);
            if (parentComment == null)
            {
                throw new ArgumentException("Parent comment does not exist");
            }
            var comment = _mapper.Map<Comment>(replyCommentDto);
            comment.ThreadId = parentComment.ThreadId;
            comment.UserId = userId;
            comment.ParentCommentId = parentCommentId;
            comment.CreatedAt = DateTime.UtcNow;
            await _commentRepository.AddComment(comment);
            var user = await _userRepository.GetUserById(userId);
            user.TotalComments += 1;
            await _userRepository.UpdateUser(user);
            return _mapper.Map<CommentDto>(comment)!;
        } 
    }
}
