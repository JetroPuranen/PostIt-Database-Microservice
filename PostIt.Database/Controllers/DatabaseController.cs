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

        public DatabaseController(
            IUserService userService,
            IPostService postService,
            IFollowerService followerService,
            IUnfollowService unfollowService)
        {
            _userService = userService;
            _postService = postService;
            _followerService = followerService;
            _unfollowService = unfollowService;
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
    }
}
