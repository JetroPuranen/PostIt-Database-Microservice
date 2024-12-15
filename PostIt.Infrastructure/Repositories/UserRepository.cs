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
            var user = await _context.Users
                .Include(u => u.Followers)  // Load Followers relationship
                .Include(u => u.Following)  // Load Following relationship
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                
                await _context.Entry(user)
                    .Collection(u => u.Followers)
                    .Query()
                    .Include(f => f.Follower) // Load Follower users
                    .LoadAsync();

                await _context.Entry(user)
                    .Collection(u => u.Following)
                    .Query()
                    .Include(f => f.Following) // Load Following users
                    .LoadAsync();

                // Remove self-references from Followers and Following
                user.Followers = user.Followers
                    .Where(f => f.FollowerId != id)
                    .ToList();

                user.Following = user.Following
                    .Where(f => f.FollowingId != id)
                    .ToList();
            }

            return user;
        }








        public async Task<IEnumerable<Users>> GetUsersByUsernameAsync(string username)
        {
            return await _context.Users
                .Where(u => u.Username.Contains(username)) // Use Contains to allow partial matches
                .ToListAsync();
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
        public async Task UpdateFollowerAsync(Guid followerId, Guid userId)
        {
            // Fetch the user and follower from the database
            var userToFollow = await _context.Users
                .Include(u => u.Followers)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var follower = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == followerId);

            if (userToFollow == null || follower == null)
            {
                throw new ArgumentException("User or Follower not found.");
            }

            
            if (_context.Entry(userToFollow).State == EntityState.Detached)
            {
                _context.Attach(userToFollow);
            }

            if (_context.Entry(follower).State == EntityState.Detached)
            {
                _context.Attach(follower);
            }

            // Check if the follower relationship already exists
            var existingFollower = await _context.UserFollowers
                .FirstOrDefaultAsync(uf => uf.FollowerId == followerId && uf.FollowingId == userId);

            if (existingFollower == null)
            {
                // Create a new UserFollowers entry for the relationship
                var newFollower = new UserFollowers
                {
                    FollowerId = followerId,
                    FollowingId = userId
                };

                _context.UserFollowers.Add(newFollower);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUnfollowAsync(Guid userId, Guid unfollowUserId)
        {
            // Fetch the user and unfollow target from the database, including Following and Followers
            var user = await _context.Users
                .Include(u => u.Following)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var unfollowUser = await _context.Users
                .Include(u => u.Followers)
                .FirstOrDefaultAsync(u => u.Id == unfollowUserId);

            if (user == null || unfollowUser == null)
            {
                throw new ArgumentException("User or unfollow target not found.");
            }

            // Ensure the user is actually following the unfollow user
            var followerRelationship = await _context.UserFollowers
                .FirstOrDefaultAsync(uf => uf.FollowerId == userId && uf.FollowingId == unfollowUserId);

            // Remove the relationship from the join table (UserFollowers)
            _context.UserFollowers.Remove(followerRelationship);

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
