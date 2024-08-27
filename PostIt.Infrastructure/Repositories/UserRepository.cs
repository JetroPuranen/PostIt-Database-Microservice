using PostIt.Domain.Entities;
using PostIt.Domain.Interfaces;
using PostIt.Infrastructure.Data;


namespace PostIt.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PostItDbContext _context;

        public UserRepository(PostItDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
