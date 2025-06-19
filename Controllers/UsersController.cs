using ForumWebsite.Dtos.Users;
using ForumWebsite.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForumWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            var result = await _userService.GetCurrentUser(userId);
            return Ok(result);
        }

        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserProfile([FromBody] UpdateCurrentUserDto updateCurrentUserDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            await _userService.UpdateCurrentUser(updateCurrentUserDto, userId);
            return Ok("Update profile successfully");
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var result = await _userService.GetUserById(userId);
            return Ok(result);
        }

        [HttpGet("get-all-user")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _userService.GetAllUser();
            return Ok(result);
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUser(userId);
            return Ok("delete user succesfully");
        }
        [HttpPost("ban-user")]
        public async Task<IActionResult> BanUser(int userId)
        {
            await _userService.BanUser(userId);
            return Ok("user banned succesfully");
        }
    }
}
