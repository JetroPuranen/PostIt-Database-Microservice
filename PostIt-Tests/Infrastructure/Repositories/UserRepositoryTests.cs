using Microsoft.EntityFrameworkCore;
using PostIt.Domain.Entities;
using PostIt.Infrastructure.Data;
using PostIt.Infrastructure.Repositories;

namespace Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly PostItDbContext _context;
        private readonly UserRepository _repository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<PostItDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new PostItDbContext(options);
            _repository = new UserRepository(_context);
        }

        [Fact]
        public async Task AddAsync_AddsUserToDatabase()
        {
            
            var user = new Users
            {
                Username = "testuser",
                Password = "password123",
                FirstName = "John",
                SurName = "Doe",
                EmailAddress = "john.doe@example.com"
            };

            
            await _repository.AddAsync(user);
            var savedUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == "testuser");

            
            Assert.NotNull(savedUser);
            Assert.Equal("testuser", savedUser.Username);
        }
        [Fact]
        public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
        {
         
            var user = new Users
            {
                Id = Guid.NewGuid(),
                Username = "testuser2",
                Password = "password123",
                FirstName = "Jane",
                SurName = "Doe",
                EmailAddress = "jane.doe@example.com"
            };
            await _repository.AddAsync(user);

           
            var retrievedUser = await _repository.GetUserByIdAsync(user.Id);

           
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.Id, retrievedUser.Id);
            Assert.Equal(user.Username, retrievedUser.Username);
        }

        [Fact]
        public async Task GetUserByUsernameAsync_ReturnsUser_WhenUserExists()
        {
         
            var user = new Users
            {
                Username = "testuser3",
                Password = "password123",
                FirstName = "Jim",
                SurName = "Beam",
                EmailAddress = "jim.beam@example.com"
            };
            await _repository.AddAsync(user);

            
            var retrievedUser = await _repository.GetUserByUsernameAsync("testuser3");

           
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.Username, retrievedUser.Username);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesUserInDatabase()
        {
          
            var user = new Users
            {
                Username = "testuser4",
                Password = "password123",
                FirstName = "Sam",
                SurName = "Smith",
                EmailAddress = "sam.smith@example.com"
            };
            await _repository.AddAsync(user);

            
            user.FirstName = "Samuel";
            await _repository.UpdateAsync(user);
            var updatedUser = await _repository.GetUserByUsernameAsync("testuser4");

            
            Assert.NotNull(updatedUser);
            Assert.Equal("Samuel", updatedUser.FirstName);
        }
        [Fact]
        public async Task DeleteUserAsync_RemovesUser_WhenUserExists()
        {
        
            var user = new Users
            {
                Username = "testuserToDelete",
                Password = "password123",
                FirstName = "ToDelete",
                SurName = "User",
                EmailAddress = "todelete.user@example.com"
            };
            await _repository.AddAsync(user);

       
            await _repository.DeleteUserAsync(user.Id);

        
            var deletedUser = await _context.Users.FindAsync(user.Id);
            Assert.Null(deletedUser);
        }

        [Fact]
        public async Task DeleteUserAsync_DoesNothing_WhenUserDoesNotExist()
        {
           
            var nonExistentUserId = Guid.NewGuid();

            await _repository.DeleteUserAsync(nonExistentUserId);

            var userCount = await _context.Users.CountAsync();
            Assert.Equal(1, userCount); 
        }
    }
}
