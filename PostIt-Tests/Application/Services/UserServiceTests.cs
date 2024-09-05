using Moq;
using PostIt.Application.Dto;
using PostIt.Application.Interfaces;
using PostIt.Application.Services;
using PostIt.Domain.Entities;
using PostIt.Domain.Interfaces;


namespace Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task AddUserAsync_CallsRepositoryAddAsync_WithCorrectUser()
        {
            
            var userDto = new UserDto
            {
                Username = "testuser",
                Password = "password123",
                FirstName = "John",
                SurName = "Doe",
                EmailAddress = "john.doe@example.com"
            };

        
            await _userService.AddUserAsync(userDto);

                 _userRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Users>(u =>
                u.Username == userDto.Username &&
                u.Password == userDto.Password &&
                u.FirstName == userDto.FirstName &&
                u.SurName == userDto.SurName &&
                u.EmailAddress == userDto.EmailAddress
            )), Times.Once);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsUserDetailDto_WhenUserExists()
        {
            
            var userId = Guid.NewGuid();
            var user = new Users
            {
                Id = userId,
                Username = "testuser",
                FirstName = "John",
                SurName = "Doe",
            };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);

           
            var result = await _userService.GetUserByIdAsync(userId);

            Assert.NotNull(result);
            Assert.IsType<UserDetailDto>(result);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.SurName, result.SurName);
            Assert.Equal(user.ProfilePicture, result.ProfilePicture);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsNull_WhenUserDoesNotExist()
        {
            
            var userId = Guid.NewGuid();

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync((Users?)null);

            var result = await _userService.GetUserByIdAsync(userId);
            
            Assert.Null(result);
        }
        [Fact]
        public async Task DeleteUserAsync_CallsRepositoryDeleteAsync_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var user = new Users
            {
                Id = userId,
                Username = "testuser",
                FirstName = "test",
                SurName = "test",
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);

            
            await _userService.DeleteUserAsync(userId);

            _userRepositoryMock.Verify(repo => repo.DeleteUserAsync(userId), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ThrowsArgumentException_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync((Users?)null);

           
            await Assert.ThrowsAsync<ArgumentException>(() => _userService.DeleteUserAsync(userId));
        }
    }
}

