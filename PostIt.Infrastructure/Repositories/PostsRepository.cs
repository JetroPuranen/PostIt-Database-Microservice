﻿using PostIt.Domain.Entities;
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
            await _context.SaveChangesAsync();
        }

        public async Task<Posts?> GetByIdAsync(Guid id)
        {
            return await _context.Posts.FindAsync(id);
        }
    }
}
