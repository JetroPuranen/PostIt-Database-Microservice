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
    }
}
