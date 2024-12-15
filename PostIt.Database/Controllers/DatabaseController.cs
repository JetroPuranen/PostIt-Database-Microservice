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
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Call the authentication service and return the UserId instead of token
            var userId = await _authService.AuthenticateAsync(loginDto.Username, loginDto.Password);

            if (userId == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Return UserId as the response
            return Ok(new { UserId = userId });
        }

        [HttpPost("addUser")]
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
        public async Task<IActionResult> AddPost([FromBody] PostDto postDto)
        {
            if (postDto == null)
            {
                return BadRequest("Post data is null.");
            }

            await _postService.AddPostAsync(postDto);
            return Ok("Post created successfully.");
        }

        [HttpPut("updatePost/{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] PostDto postDto)
        {
            if (postDto == null)
            {
                return BadRequest("Post data is null.");
            }

            var result = await _postService.UpdatePostAsync(id, postDto);
            if (!result)
            {
                return NotFound("Post not found.");
            }

            return Ok("Post updated successfully.");
        }

        [HttpPost("addFollower")]
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
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok("User deleted successfully.");
        }
        [HttpGet("getPost/{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if(post == null)
            {
                return NotFound("Post not found with id" + id);
            }
            return Ok(post);
        }
        [HttpGet("getPostsByUser/{userId}")]
        public async Task<IActionResult> GetPostsByUser(Guid userId)
        {
            var posts = await _postService.GetPostsByUserIdAsync(userId);

            if (posts == null || !posts.Any())
            {
                return NotFound($"No posts found for user with ID {userId}");
            }

            return Ok(posts);
        }

        [HttpGet("getUserByUsername/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            username = username.Trim(); // Remove leading and trailing whitespace
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Username cannot be empty.");
            }

            var users = await _userService.GetUsersByUsernameAsync(username);

            if (users == null || !users.Any()) // Checking for an empty list
            {
                return NotFound($"No users found matching '{username}'.");
            }

            return Ok(users); // Return the list of users
        }
    }
}
