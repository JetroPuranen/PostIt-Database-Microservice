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

        public DatabaseController(IUserService userService, IPostService postService)
        {
            _userService = userService;
            _postService = postService;
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
    }
}
