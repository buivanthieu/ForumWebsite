using ForumWebsite.Services.Votes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForumWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly ICommentVoteService _commentVoteService;
        private readonly IForumThreadVoteService _forumThreadVoteService;

        public VotesController (ICommentVoteService commentVoteService, IForumThreadVoteService forumThreadVoteService)
        {
            _commentVoteService = commentVoteService;
            _forumThreadVoteService = forumThreadVoteService;
        }

        [HttpPost("vote-comment")]
        [Authorize]
        public async Task<IActionResult> VoteComment(int commentId, bool isVoteUp)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            await _commentVoteService.VoteComment(userId, commentId, isVoteUp);
            return Ok();
        }

        [HttpPost("vote-thread")]
        [Authorize]
        public async Task<IActionResult> VoteThread(int threadId, bool isVoteUp)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            await _forumThreadVoteService.VoteThread(userId, threadId, isVoteUp);
            return Ok();
        }


        [HttpGet("get-comment-vote")]
        public async Task<IActionResult> CountCommentVote(int commentId)
        {
            var result = await _commentVoteService.GetCommentVote(commentId);
            return Ok(result);
        }

        [HttpGet("get-thread-vote")]
        public async Task<IActionResult> ForumThreadVote(int threadId)
        {
            var result = await _forumThreadVoteService.GetForumThreadVote(threadId);
            return Ok(result);

        }
    }
}
