using PostIt.Application.Dto;
using PostIt.Application.Interfaces;
using PostIt.Domain.Entities;
using PostIt.Domain.Interfaces;


namespace PostIt.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task AddPostAsync(PostDto postDto)
        {
            // Map the PostDto to the Posts entity
            var post = new Posts
            {
                UserId = postDto.UserId,
                ImageData = postDto.ImageData,
                Caption = postDto.Caption,
                User = postDto.User
            };

            // Add the post to the repository
            await _postRepository.AddAsync(post);
        }
    }
}
