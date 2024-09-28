using PostIt.Application.Interfaces;
using PostIt.Domain.Interfaces;


namespace PostIt.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Modify AuthenticateAsync to return userId instead of token
        public async Task<Guid?> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !VerifyPassword(password, user.Password))
            {
                return null;
            }

            // If the password matches, return the UserId
            return user.Id;
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            
            return true; 
        }
    }

    
}
