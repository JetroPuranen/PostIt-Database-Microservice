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
            if (unfollowDto == null || unfollowDto.UserId == Guid.Empty || unfollowDto.UnfollowUserId == Guid.Empty)
            {
                throw new ArgumentException("Invalid follower data provided.");
            }

            // Fetch both users from the repository
            var user = await _userRepository.GetUserByIdAsync(unfollowDto.UserId);
            var unfollowUser = await _userRepository.GetUserByIdAsync(unfollowDto.UnfollowUserId);

            if (user == null || unfollowUser == null)
            {
                throw new Exception("User or unfollow user not found.");
            }

            

            // Delegate the actual update to the repository
            await _userRepository.UpdateUnfollowAsync(user.Id, unfollowUser.Id);
        }

    }
}
