namespace PostIt.Domain.Entities
{
    public class Posts : BaseEntity
    {
        
        public Guid UserId { get; set; }
        public byte[] ImageData { get; set; }
        public string Caption { get; set; }

        public int LikeCount { get; set; }
        public ICollection<PostComment> Comments { get; set; } = new List<PostComment>();
        public ICollection<PostLike> Likes { get; set; } = new List<PostLike>();

        public DateTime CreatedAt { get; set; } = new DateTime();
        public Posts ToEntity()
        {
            return new Posts
            {
                Id = Id,
                UserId = this.UserId,
                ImageData = this.ImageData,
                Caption = this.Caption,
                Comments = this.Comments,
                LikeCount = this.LikeCount, 
                Likes = this.Likes,
                CreatedAt = this.CreatedAt
            };
        }
    }
    public class PostComment
    {
        public Guid Id { get; set; } // Unique identifier for the comment
        public Guid PostId { get; set; }
        public Posts Post { get; set; }
        public Guid UserId { get; set; }
        public Users User { get; set; }
        public string Comment { get; set; }
    }

    public class PostLike
    {
        public Guid Id { get; set; } // Unique identifier for the like
        public Guid PostId { get; set; }
        public Posts Post { get; set; }
        public Guid UserId { get; set; }
        public Users User { get; set; }
    }
}
