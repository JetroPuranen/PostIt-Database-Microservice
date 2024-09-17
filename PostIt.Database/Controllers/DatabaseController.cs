using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostIt.Application.Dto;
using PostIt.Application.Interfaces;

namespace PostIt.Database.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly IFollowerService _followerService;
        private readonly IUnfollowService _unfollowService;
        private readonly IAuthService _authService;

        public DatabaseController(
            IUserService userService,
            IPostService postService,
            IFollowerService followerService,
            IUnfollowService unfollowService,
            IAuthService authService)
        {
            _userService = userService;
            _postService = postService;
            _followerService = followerService;
            _unfollowService = unfollowService;
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _authService.AuthenticateAsync(loginDto.Username, loginDto.Password);
            if (token == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(new { Token = token });
        }

        [HttpPost("addUser")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null.");
            }

            await _userService.AddUserAsync(userDto);
            return Ok("User created successfully.");
        }

        [HttpPost("addPost")]
        [Authorize]
        public async Task<IActionResult> AddPost([FromBody] PostDto postDto)
        {
            if (postDto == null)
            {
                return BadRequest("Post data is null.");
            }

            await _postService.AddPostAsync(postDto);
            return Ok("Post created successfully.");
        }

        [HttpPost("addFollower")]
        [Authorize]
        public async Task<IActionResult> AddFollower([FromBody] FollowerDto followerDto)
        {
            if (followerDto == null)
            {
                return BadRequest("Follower data is null.");
            }

            await _followerService.AddFollowerAsync(followerDto);
            return Ok("Follower added successfully.");
        }

        [HttpPost("unfollowUser")]
        [Authorize]
        public async Task<IActionResult> UnfollowUser([FromBody] UnfollowDto unfollowDto)
        {
            if (unfollowDto == null)
            {
                return BadRequest("Unfollow data is null.");
            }

            await _unfollowService.RemoveFollowerAsync(unfollowDto);
            return Ok("Unfollowed user successfully.");
        }

        [HttpGet("getUser/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        [HttpDelete("deleteUser/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok("User deleted successfully.");
        }
    }
}
