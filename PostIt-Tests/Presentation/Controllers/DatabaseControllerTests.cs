using Microsoft.AspNetCore.Mvc;
using Moq;
using PostIt.Application.Dto;
using PostIt.Application.Interfaces;
using PostIt.Database.Controllers;


namespace Tests.Controllers
{
    public class DatabaseControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IPostService> _postServiceMock;
        private readonly Mock<IFollowerService> _followerServiceMock;  
        private readonly Mock<IUnfollowService> _unfollowServiceMock;  
        private readonly DatabaseController _controller;

        public DatabaseControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _postServiceMock = new Mock<IPostService>();
            _followerServiceMock = new Mock<IFollowerService>();  
            _unfollowServiceMock = new Mock<IUnfollowService>();  

            
            _controller = new DatabaseController(
                _userServiceMock.Object,
                _postServiceMock.Object,
                _followerServiceMock.Object,
                _unfollowServiceMock.Object);
        }

        [Fact]
        public async Task AddUser_ReturnsBadRequest_WhenUserDtoIsNull()
        {
            
            var result = await _controller.AddUser(null);

            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User data is null.", badRequestResult.Value);
        }

        [Fact]
        public async Task AddUser_ReturnsOk_WhenUserIsAddedSuccessfully()
        {
            
            var userDto = new UserDto { Username = "testuser", Password = "password123" };

            _userServiceMock.Setup(service => service.AddUserAsync(userDto))
                            .Returns(Task.CompletedTask);

            
            var result = await _controller.AddUser(userDto);

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User created successfully.", okResult.Value);
        }
    }
}
