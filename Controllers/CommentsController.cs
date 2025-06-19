using ForumWebsite.Dtos.Comments;
using ForumWebsite.Services.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForumWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentsController (ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("comment-in-thread")]
        public async Task<IActionResult> GetCommentInThread(int threadId)
        {
            var result = await _commentService.GetCommentsByThreadId(threadId);
            return Ok(result);
        }

        [HttpGet("comment-infor")]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            var result = await _commentService.GetCommentById(commentId);
            return Ok(result);
        }
        [HttpPost("create-comment")]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto commentDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if(!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await _commentService.CreateComment(commentDto, userId);
            return Ok(result);
        }

        [HttpPost("reply-comment")]
        [Authorize]
        public async Task<IActionResult> ReplyComment([FromBody] ReplyCommentDto replyCommentDto, int parentCommentId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(!int.TryParse (userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            var result = await _commentService.ReplyComment(replyCommentDto, userId, parentCommentId);
            return Ok(result);
        }
        [HttpPut("update-comment")]
        [Authorize]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDto updateCommentDto, int commentId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(!int.TryParse (userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            await _commentService.UpdateComment(commentId, updateCommentDto, userId);
            return Ok("update successfully");
        }

        [HttpDelete("delete-comment")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            await _commentService.DeleteComment(commentId, userId);
            return Ok("Delete comment succesfully");
        }
        
    }
}
