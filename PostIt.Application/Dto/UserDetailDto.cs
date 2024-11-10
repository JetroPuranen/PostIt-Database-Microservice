namespace PostIt.Application.Dto
{
    public class UserDetailDto
    {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }

        public DateTime? BirhtDay { get; set; }
        public List<SimpleUserDto> Followers { get; set; } = new();
        public List<SimpleUserDto> Following { get; set; } = new();

        public byte[]? ProfilePicture { get; set; }
        
    }

    public class SimpleUserDto
    {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
    }
}

