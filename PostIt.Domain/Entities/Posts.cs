namespace PostIt.Domain.Entities
{
    public class Posts : BaseEntity
    {
        public Guid UserId { get; set; }
        public byte[] ImageData { get; set; }  
        public string Caption { get; set; }
        public List<string>? Comments { get; set; }
        public int LikeCount { get; set; }
        public List<string>? WhoHasLiked  {get; set;}


        public Posts ToEntity()
        {
            return new Posts
            {
                UserId = this.UserId,
                ImageData = this.ImageData,
                Caption = this.Caption,
                Comments = this.Comments,
                LikeCount = this.LikeCount, 
                WhoHasLiked = this.WhoHasLiked
            };
        }
    }
}
