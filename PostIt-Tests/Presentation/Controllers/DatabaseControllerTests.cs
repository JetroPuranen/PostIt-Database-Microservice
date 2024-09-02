using Microsoft.AspNetCore.Mvc;
using Moq;
using PostIt.Application.Dto;
using PostIt.Application.Interfaces;
using PostIt.Database.Controllers;
using Xunit;

namespace Tests.Controllers
{
    public class DatabaseControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly DatabaseController _controller;

        public DatabaseControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new DatabaseController(_userServiceMock.Object);
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
