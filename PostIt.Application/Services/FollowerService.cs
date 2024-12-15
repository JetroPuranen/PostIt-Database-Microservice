using PostIt.Application.Dto;
using PostIt.Application.Interfaces;
using PostIt.Domain.Interfaces;
using System.Linq;

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
            if (followerDto == null || followerDto.UserId == Guid.Empty || followerDto.FollowerUserId == Guid.Empty)
            {
                throw new ArgumentException("Invalid follower data provided.");
            }

            // Ensure the user is not following themselves
            if (followerDto.UserId == followerDto.FollowerUserId)
            {
                throw new InvalidOperationException("User cannot follow themselves.");
            }

            // Fetch both users from the repository
            var userToFollow = await _userRepository.GetUserByIdAsync(followerDto.UserId);
            var follower = await _userRepository.GetUserByIdAsync(followerDto.FollowerUserId);

            if (userToFollow == null || follower == null)
            {
                throw new Exception("User or follower not found.");
            }

            // Avoid adding the same follower again
            var existingFollower = userToFollow.Followers
                .FirstOrDefault(uf => uf.FollowerId == follower.Id);

            if (existingFollower != null)
            {
                throw new InvalidOperationException("Follower already exists.");
            }

            // Delegate the actual update to the UpdateFollowerAsync method
            await _userRepository.UpdateFollowerAsync(followerDto.UserId, followerDto.FollowerUserId);
        }

    }
}


