namespace PostIt.Domain.Entities
{
    public class UserFollowers
    {
        public Guid FollowerId { get; set; }
        public Users Follower { get; set; }

        public Guid FollowingId { get; set; }
        public Users Following { get; set; }
    }
}
