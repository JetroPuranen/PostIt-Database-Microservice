using PostIt.Application.Dto;
using PostIt.Application.Interfaces;
using PostIt.Domain.Entities;
using PostIt.Domain.Interfaces;
using System.Linq;

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
                BirthDay = userDto.BirthDay.Date,
                ProfilePicture = userDto.ProfilePicture
            };

            await _userRepository.AddAsync(user);
        }
        public async Task<UserDetailDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserDetailDto
            {
                UserId = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                SurName = user.SurName,
                BirhtDay = user.BirthDay,
                Followers = user.Followers.Select(f => new SimpleUserDto
                {
                    UserId = f.Follower.Id,        
                    Username = f.Follower.Username 
                }).ToList(),
                Following = user.Following.Select(f => new SimpleUserDto
                {
                    UserId = f.Following.Id,        
                    Username = f.Following.Username 
                }).ToList(),
                ProfilePicture = user.ProfilePicture,
            };
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            await _userRepository.DeleteUserAsync(id);
        }
        public async Task<List<UserDetailDto?>> GetUsersByUsernameAsync(string username)
        {
            var users = await _userRepository.GetUsersByUsernameAsync(username); // Assuming this returns List<Users>
            var userDetails = users.Select(user => new UserDetailDto
            {
                UserId = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                SurName = user.SurName,
                ProfilePicture = user.ProfilePicture
            }).ToList();

            return userDetails; // Return the list of UserDetailDto
        }

    }
}
