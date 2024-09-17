using Microsoft.EntityFrameworkCore;
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
        public async Task<Users?> GetUserByIdAsync(Guid id)  
        {
            return await _context.Users
                .Where(u => u.Id == id)  
                .Select(u => new Users
                {
                    Id = u.Id,
                    Username = u.Username,
                    FirstName = u.FirstName,
                    SurName = u.SurName,
                    ProfilePicture = u.ProfilePicture
                })
                .FirstOrDefaultAsync();
        }
        public async Task<Users> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task UpdateAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
