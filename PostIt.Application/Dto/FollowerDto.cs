namespace PostIt.Application.Dto
{
    public class FollowerDto
    {
        public Guid UserId { get; set; }  //user who is going to follow someone
        public Guid FollowerUserId { get; set; }  //username of the user they want to follow
    }
}
