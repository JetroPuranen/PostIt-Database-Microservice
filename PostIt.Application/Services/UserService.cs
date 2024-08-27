using PostIt.Application.Dto;
using PostIt.Application.Interfaces;
using PostIt.Domain.Entities;
using PostIt.Domain.Interfaces;


namespace PostIt.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            var user = new Users
            {
                Username = userDto.Username,
                Password = userDto.Password,
                FirstName = userDto.FirstName,
                SurName = userDto.SurName,
                EmailAddress = userDto.EmailAddress,
                HomeAddress = userDto.HomeAddress,
                BirthDay = userDto.BirthDay,
                ProfilePicture = userDto.ProfilePicture
            };

            await _userRepository.AddAsync(user);
        }
    }
}
