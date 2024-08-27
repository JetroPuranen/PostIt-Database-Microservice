using PostIt.Domain.Entities;


namespace PostIt.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(Users user);
    }
}
