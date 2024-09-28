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
           
            var post = new Posts
            {
                UserId = postDto.UserId,
                ImageData = postDto.ImageData,
                Caption = postDto.Caption,
                Comments = postDto.Comments,
                LikeCount = postDto.LikeCount,
                WhoHasLiked = postDto.WhoHasLiked,
            };

            
            await _postRepository.AddAsync(post);
        }

        public async Task<bool> UpdatePostAsync(Guid id, PostDto postDto)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return false;
            }

            post.Caption = postDto.Caption;
            post.Comments = postDto.Comments;
            post.LikeCount = postDto.LikeCount;
            post.WhoHasLiked = postDto.WhoHasLiked;

            await _postRepository.UpdateAsync(post);
            return true;
        }
    }
}
