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
    }
}
