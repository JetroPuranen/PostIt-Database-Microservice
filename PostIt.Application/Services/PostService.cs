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
                Comments = new List<PostComment>(), //empty list
                Likes = new List<PostLike>(),       //empty list
                LikeCount = 0                       
            };

            // Add new post to the database
            await _postRepository.AddAsync(post);
        }



        public async Task<PostDto?> GetPostByIdAsync(Guid id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null) return null;

            return new PostDto
            {
                Id = post.Id,
                UserId = post.UserId,
                Caption = post.Caption,
                ImageData = post.ImageData,
                Comments = post.Comments.Select(c => new CommentDto
                {
                    UserId = c.UserId,
                    Comment = c.Comment
                }).ToList(),
                LikeCount = post.Likes.Count,
                WhoHasLiked = post.Likes.Select(l => l.UserId).ToList(),
                WhoHasCommented = post.Comments.Select(c => c.UserId).ToList()
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
                ImageData = post.ImageData,
                Comments = post.Comments.Select(c => new CommentDto
                {
                    UserId = c.UserId,
                    Comment = c.Comment
                }).ToList(),
                LikeCount = post.Likes.Count,
                WhoHasLiked = post.Likes.Select(l => l.UserId).ToList(),
                WhoHasCommented = post.Comments.Select(c => c.UserId).ToList()
            });
        }

        public async Task UpdatePostAsync(PostUpdateDto postUpdateDto)
        {
            
            if (postUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(postUpdateDto), "Post update data cannot be null.");
            }

            
            var post = await _postRepository.GetPostByIdAsync(postUpdateDto.Id);
            if (post == null)
            {
                throw new Exception($"Post with ID {postUpdateDto.Id} not found.");
            }

            if (!string.IsNullOrWhiteSpace(postUpdateDto.Caption))
            {
                post.Caption = postUpdateDto.Caption;
            }
            //handle likes
            if (!string.IsNullOrWhiteSpace(postUpdateDto.LikedByUserId))
            {
                var userId = Guid.Parse(postUpdateDto.LikedByUserId);

                if (!post.Likes.Any(like => like.UserId == userId))
                {
                    post.Likes.Add(new PostLike
                    {
                        Id = Guid.NewGuid(),
                        PostId = post.Id,
                        UserId = userId
                    });

                    post.LikeCount++;
                }
            }

            //handle comments
            if (postUpdateDto.Comment != null)
            {
                if (postUpdateDto.Comment.UserId == Guid.Empty)
                {
                    throw new ArgumentException("Comment UserId cannot be empty.", nameof(postUpdateDto.Comment.UserId));
                }

                if (string.IsNullOrWhiteSpace(postUpdateDto.Comment.Comment))
                {
                    throw new ArgumentException("Comment text cannot be empty.", nameof(postUpdateDto.Comment.Comment));
                }

                post.Comments.Add(new PostComment
                {
                    Id = Guid.NewGuid(),
                    PostId = post.Id,
                    UserId = postUpdateDto.Comment.UserId,
                    Comment = postUpdateDto.Comment.Comment
                });
            }

            await _postRepository.UpdateAsync(post);
        }


    }
}