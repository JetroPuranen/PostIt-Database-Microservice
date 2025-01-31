using PostIt.Application.Dto;


namespace PostIt.Application.Interfaces
{
    public interface IPostService
    {
        Task AddPostAsync(PostDto postDto);
        Task<PostDto?> GetPostByIdAsync(Guid id);
        Task<IEnumerable<PostDto?>> GetPostsByUserIdAsync(Guid userId);
        Task UpdatePostAsync(PostUpdateDto postUpdateDto);
    }
}
