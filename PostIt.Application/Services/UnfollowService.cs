using PostIt.Application.Dto;
using PostIt.Application.Interfaces;
using PostIt.Domain.Interfaces;


namespace PostIt.Application.Services
{
    public class UnfollowService : IUnfollowService
    {
        private readonly IUserRepository _userRepository;

        public UnfollowService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RemoveFollowerAsync(UnfollowDto unfollowDto)
        {
            if (unfollowDto == null || string.IsNullOrEmpty(unfollowDto.Username) || string.IsNullOrEmpty(unfollowDto.UnfollowUsername))
            {
                throw new ArgumentException("Invalid unfollow data provided.");
            }

            
            var user = await _userRepository.GetUserByUsernameAsync(unfollowDto.Username);
            var unfollowUser = await _userRepository.GetUserByUsernameAsync(unfollowDto.UnfollowUsername);

            if (user == null || unfollowUser == null)
            {
                throw new Exception("User or unfollow user not found.");
            }

            
            var userFollowing = user.Following.FirstOrDefault(f => f.Username == unfollowUser.Username);
            if (userFollowing == null)
            {
                throw new InvalidOperationException("The user is not following the specified user.");
            }

            
            user.Following.Remove(userFollowing);
            unfollowUser.Followers.Remove(unfollowUser.Followers.First(f => f.Username == user.Username));

           
            await _userRepository.UpdateAsync(user);
            await _userRepository.UpdateAsync(unfollowUser);
        }
    }
}
