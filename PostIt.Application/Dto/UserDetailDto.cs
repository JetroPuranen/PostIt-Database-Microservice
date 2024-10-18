namespace PostIt.Application.Dto
{
    public class UserDetailDto
    {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}
