namespace PostIt.Application.Dto
{
    public class UnfollowDto
    {
        public Guid UserId { get; set; }  //user who wants to unfollow
        public Guid UnfollowUserId { get; set; }  //user to be unfollowed
    }
}
