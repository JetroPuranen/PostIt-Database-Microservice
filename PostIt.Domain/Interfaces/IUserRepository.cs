
using PostIt.Domain.Entities;

namespace PostIt.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> GetUserByUsernameAsync(string username);
        Task UpdateAsync(Users user);
        Task AddAsync(Users user);

        Task<Users?> GetUserByIdAsync(Guid id);
    }
}
