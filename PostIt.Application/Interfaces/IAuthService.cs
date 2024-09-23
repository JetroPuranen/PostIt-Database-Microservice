namespace PostIt.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Guid?> AuthenticateAsync(string username, string password);  
    }
}
