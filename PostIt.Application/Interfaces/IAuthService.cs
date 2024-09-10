using PostIt.Application.Dto;

namespace PostIt.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string?> AuthenticateAsync(string username, string password);
        
    }
}
