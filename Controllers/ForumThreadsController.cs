using ForumWebsite.Dtos.ForumThreads;
using ForumWebsite.Services.ForumThreads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForumWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumThreadsController : ControllerBase
    {
        private readonly IForumThreadService _forumThreadService;
        public ForumThreadsController (IForumThreadService forumThreadService)
        {
            _forumThreadService = forumThreadService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllThreads()
        {
            var result = await _forumThreadService.GetThreads();
            return Ok(result);
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetThreadById(int threadId)
        {
            var result = await _forumThreadService.GetThreadById(threadId);
            return Ok(result);
        }
        [HttpGet("search-by-name")]
        public async Task<IActionResult> SearchThreadByName(string name)
        {
            var result = await _forumThreadService.SearchThreadByName(name);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("create-thread")]
        public async Task<IActionResult> CreateThread([FromBody] CreateForumThreadDto createForumThreadDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //int userId = 1;

            //if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var parsedUserId))
            //{
            //    userId = parsedUserId;
            //}
            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            var result = await _forumThreadService.CreateThread(createForumThreadDto, userId);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("update-thread")]
        public async Task<IActionResult> UpdateThread(int threadId,[FromBody] UpdateForumThreadDto updateForumThreadDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            await _forumThreadService.UpdateThread(threadId, updateForumThreadDto, userId);
            return Ok("Updated successfully");
        }

        [HttpDelete("delete-thread")]
        [Authorize]
        public async Task<IActionResult> DeleteThread(int threadId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            await _forumThreadService.DeleteThread(threadId, userId);
            return Ok("delete successfullt");
        }
    }
}
