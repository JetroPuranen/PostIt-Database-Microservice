using Microsoft.EntityFrameworkCore;
using PostIt.Domain.Entities;
using PostIt.Domain.Interfaces;
using PostIt.Infrastructure.Data;


namespace PostIt.Infrastructure.Repositories
{
    public class PostsRepository : IPostRepository
    {
        private readonly PostItDbContext _context;

        public PostsRepository(PostItDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Posts post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Posts post)
        {
            _context.Posts.Update(post);

            foreach (var like in post.Likes)
            {
                if (_context.PostLikes.Any(l => l.Id == like.Id)) continue;
                _context.PostLikes.Add(like);
            }

            foreach (var comment in post.Comments)
            {
                if (_context.PostComments.Any(c => c.Id == comment.Id)) continue;
                _context.PostComments.Add(comment);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Posts?> GetPostByIdAsync(Guid id)
        {
            return await _context.Posts
                                 .Include(p => p.Comments)
                                 .Include(p => p.Likes)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Posts?>> GetPostsByUserIdAsync(Guid userId)
        {
            return await _context.Posts
                                 .Where(p => p.UserId == userId)
                                 .Include(p => p.Comments)
                                 .Include(p => p.Likes)
                                 .ToListAsync();
        }
    }
}
