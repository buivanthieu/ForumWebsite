using AutoMapper;
using ForumWebsite.Dtos.Users;
using ForumWebsite.Dtos.Votes;
using ForumWebsite.Models;
using ForumWebsite.Repositories.Comments;
using ForumWebsite.Repositories.Users;
using ForumWebsite.Repositories.Votes;

namespace ForumWebsite.Services.Votes
{
    public class CommentVoteService : ICommentVoteService
    {
        private readonly ICommentVoteRepository _commentVoteRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        public CommentVoteService(ICommentVoteRepository commentVoteRepository, IMapper mapper, 
            IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _commentVoteRepository = commentVoteRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }


        public async Task VoteComment(int userId, int commentId, bool isUpVote)
        {
            var existingCommentVote = await _commentVoteRepository.GetCommentVote(userId, commentId);
            if (existingCommentVote != null)
            {
                if (existingCommentVote.IsUpvote == isUpVote)
                {
                    await _commentVoteRepository.DeleteCommentVote(existingCommentVote);
                }
                else
                {
                    existingCommentVote.IsUpvote = isUpVote;
                    existingCommentVote.VotedAt = DateTime.UtcNow;
                    await _commentVoteRepository.UpdateCommentVote(existingCommentVote);
                }
            }
            else
            {
                var newVote = new CommentVote
                {
                    UserId = userId,
                    CommentId = commentId,
                    IsUpvote = isUpVote,
                    VotedAt = DateTime.UtcNow
                };
                await _commentVoteRepository.AddCommentVote(newVote);
            }

            await _commentVoteRepository.UpdateCommentReputation(commentId);
            var ownerId = await _commentVoteRepository.GetCommentOwnerId(commentId);
            if (ownerId.HasValue)
            {
                await _userRepository.UpdateUserReputation(ownerId.Value);
            }
        }

        public async Task<CommentVoteDto> GetCommentVote(int commentId)
        {
            var comment  = await _commentRepository.GetCommentById(commentId);
            var userVoteUp = await _commentVoteRepository.GetUserVoteUpComment(commentId);
            var userVoteDown = await _commentVoteRepository.GetUserVoteDownComment(commentId);
            var upDtos = _mapper.Map<ICollection<UserBaseDto>>(userVoteUp);
            var downDtos = _mapper.Map<ICollection<UserBaseDto>>(userVoteDown);
            return new CommentVoteDto
            {
                Reputation  = comment.Reputation,
                CountDownVote = downDtos.Count,
                CountUpVote = upDtos.Count,
                UsersVoteDown = downDtos,
                UsersVoteUp = upDtos,
            };
        }
    }
}
