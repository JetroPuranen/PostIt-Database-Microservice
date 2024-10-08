﻿

namespace PostIt.Application.Dto
{
    public class UserDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? EmailAddress { get; set; }
        public string? HomeAddress { get; set; }
        public DateTime BirthDay { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}
