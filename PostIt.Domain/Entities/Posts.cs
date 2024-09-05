namespace PostIt.Domain.Entities
{
    public class Posts : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual Users User { get; set; }

        public byte[] ImageData { get; set; }  
        public string Caption { get; set; }    

        
        public Posts ToEntity()
        {
            return new Posts
            {
                UserId = this.UserId,
                User = this.User,
                ImageData = this.ImageData,
                Caption = this.Caption
            };
        }
    }
}
