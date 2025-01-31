
namespace PostIt.Application.Dto
{
    public class PostUpdateDto
    {
        public Guid Id { get; set; }
        public string? Caption { get; set; } 
        public string? LikedByUserId { get; set; } 
        public CommentDto? Comment { get; set; } 
    }

    
}
