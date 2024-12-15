namespace PostIt.Domain.Entities
{
    
    public class Users : BaseEntity
    {
        public string? Username { get; set; }
        public string? Password { get; set; } // Password property
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? EmailAddress { get; set; }
        public string? HomeAddress { get; set; }
        public DateTime BirthDay { get; set; }
        public byte[]? ProfilePicture { get; set; } // Profile picture property



        public ICollection<UserFollowers> Followers { get; set; } = new List<UserFollowers>();
        public ICollection<UserFollowers> Following { get; set; } = new List<UserFollowers>();
        public virtual ICollection<Posts> Posts { get; set; } = new List<Posts>();

        // Entity creation
        public Users ToEntity()
        {
            return new Users
            {
                Username = this.Username,
                FirstName = this.FirstName,
                SurName = this.SurName,
                EmailAddress = this.EmailAddress,
                HomeAddress = this.HomeAddress,
                BirthDay = this.BirthDay,
                ProfilePicture = this.ProfilePicture,
                Password = this.Password, // Include the new property
                Followers = this.Followers,
                Following = this.Following,
                Posts = this.Posts
            };
        }
    }
}
