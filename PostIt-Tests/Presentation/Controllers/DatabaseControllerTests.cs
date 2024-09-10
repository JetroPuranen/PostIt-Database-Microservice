using Microsoft.AspNetCore.Mvc;
using Moq;
using PostIt.Application.Dto;
using PostIt.Application.Interfaces;
using PostIt.Database.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Controllers
{
    public class DatabaseControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IPostService> _postServiceMock;
        private readonly Mock<IFollowerService> _followerServiceMock;
        private readonly Mock<IUnfollowService> _unfollowServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly DatabaseController _controller;

        public DatabaseControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _postServiceMock = new Mock<IPostService>();
            _followerServiceMock = new Mock<IFollowerService>();
            _unfollowServiceMock = new Mock<IUnfollowService>();
            _authServiceMock = new Mock<IAuthService>();


            _controller = new DatabaseController(
                _userServiceMock.Object,
                _postServiceMock.Object,
                _followerServiceMock.Object,
                _unfollowServiceMock.Object,
                _authServiceMock.Object);
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

        [Fact]
        public async Task AddPost_ReturnsBadRequest_WhenPostDtoIsNull()
        {
            var result = await _controller.AddPost(null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Post data is null.", badRequestResult.Value);
        }

       

        [Fact]
        public async Task AddFollower_ReturnsBadRequest_WhenFollowerDtoIsNull()
        {
            var result = await _controller.AddFollower(null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Follower data is null.", badRequestResult.Value);
        }

        [Fact]
        public async Task AddFollower_ReturnsOk_WhenFollowerIsAddedSuccessfully()
        {
            var followerDto = new FollowerDto { Username = "Username", FollowerUsername = "FollowUsername" };

            _followerServiceMock.Setup(service => service.AddFollowerAsync(followerDto))
                                .Returns(Task.CompletedTask);

            var result = await _controller.AddFollower(followerDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Follower added successfully.", okResult.Value);
        }

        [Fact]
        public async Task UnfollowUser_ReturnsBadRequest_WhenUnfollowDtoIsNull()
        {
            var result = await _controller.UnfollowUser(null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Unfollow data is null.", badRequestResult.Value);
        }

        [Fact]
        public async Task UnfollowUser_ReturnsOk_WhenUserIsUnfollowedSuccessfully()
        {
            var unfollowDto = new UnfollowDto { Username = "Username", UnfollowUsername = "UnfollowUsername" };

            _unfollowServiceMock.Setup(service => service.RemoveFollowerAsync(unfollowDto))
                                .Returns(Task.CompletedTask);

            var result = await _controller.UnfollowUser(unfollowDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Unfollowed user successfully.", okResult.Value);
        }

        [Fact]
        public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();

            _userServiceMock.Setup(service => service.GetUserByIdAsync(userId))
                            .ReturnsAsync((UserDetailDto?)null);

            var result = await _controller.GetUserById(userId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"User with ID {userId} not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetUserById_ReturnsOk_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var userDto = new UserDetailDto
            {
                Username = "testuser",
                FirstName = "test",
                SurName = "test",
                
            };

            _userServiceMock.Setup(service => service.GetUserByIdAsync(userId))
                            .ReturnsAsync(userDto);

            var result = await _controller.GetUserById(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUserDto = Assert.IsType<UserDetailDto>(okResult.Value);
            Assert.Equal(userDto.Username, returnedUserDto.Username);
            Assert.Equal(userDto.FirstName, returnedUserDto.FirstName);
            Assert.Equal(userDto.SurName, returnedUserDto.SurName);
            Assert.Equal(userDto.ProfilePicture, returnedUserDto.ProfilePicture);
        }

        [Fact]
        public async Task DeleteUser_ReturnsOk_WhenUserIsDeletedSuccessfully()
        {
            var userId = Guid.NewGuid();

            _userServiceMock.Setup(service => service.DeleteUserAsync(userId))
                            .Returns(Task.CompletedTask);

            var result = await _controller.DeleteUser(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User deleted successfully.", okResult.Value);
        }
    }
}
