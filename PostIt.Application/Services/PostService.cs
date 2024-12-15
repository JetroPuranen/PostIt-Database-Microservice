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
            var post = await _postRepository.GetPostByIdAsync(id);
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

        public async Task<PostDto?> GetPostByIdAsync(Guid id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);

            return new PostDto
            {
                Caption = post.Caption,
                Comments = post.Comments,
                LikeCount = post.LikeCount,
                WhoHasLiked = post.WhoHasLiked,
                ImageData = post.ImageData,

            };

        }
        public async Task<IEnumerable<PostDto>> GetPostsByUserIdAsync(Guid userId)
        {
            var posts = await _postRepository.GetPostsByUserIdAsync(userId);

            return posts.Select(post => new PostDto
            {
                Id = post.Id,
                UserId = post.UserId,
                Caption = post.Caption,
                Comments = post.Comments,
                LikeCount = post.LikeCount,
                WhoHasLiked = post.WhoHasLiked,
                ImageData = post.ImageData
            });
        }
    }
}
