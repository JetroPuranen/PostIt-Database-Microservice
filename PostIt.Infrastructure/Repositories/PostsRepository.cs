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
    }
}
