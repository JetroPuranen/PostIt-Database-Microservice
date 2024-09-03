using PostIt.Application.Dto;
using PostIt.Application.Interfaces;
using PostIt.Domain.Interfaces;

namespace PostIt.Application.Services
{
    public class FollowerService : IFollowerService
    {
        private readonly IUserRepository _userRepository;

        public FollowerService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddFollowerAsync(FollowerDto followerDto)
        {
            if (followerDto == null || string.IsNullOrEmpty(followerDto.Username) || string.IsNullOrEmpty(followerDto.FollowerUsername))
            {
                throw new ArgumentException("Invalid follower data provided.");
            }

            // Get both users from the repository
            var user = await _userRepository.GetUserByUsernameAsync(followerDto.Username);
            var follower = await _userRepository.GetUserByUsernameAsync(followerDto.FollowerUsername);

            if (user == null || follower == null)
            {
                throw new Exception("User or follower not found.");
            }

            // Check if the follower is already following the user
            if (user.Followers.Any(f => f.Username == follower.Username))
            {
                // Follower already exists in the user's follower list, no need to add
                throw new InvalidOperationException("The user is already followed by this follower.");
            }

            // Check if the user already follows the follower (to prevent duplicate following)
            if (follower.Following.Any(f => f.Username == user.Username))
            {
                throw new InvalidOperationException("The follower already follows this user.");
            }

            // Add to Followers and Following lists
            user.Followers.Add(follower);
            follower.Following.Add(user);

            // Update the users in the repository
            await _userRepository.UpdateAsync(user);
            await _userRepository.UpdateAsync(follower);
        }
    }
}
