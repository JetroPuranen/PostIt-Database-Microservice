﻿
using PostIt.Domain.Entities;

namespace PostIt.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> GetUserByUsernameAsync(string username);
        Task<IEnumerable<Users>> GetUsersByUsernameAsync(string username);
        Task UpdateAsync(Users user);
        Task UpdateFollowerAsync(Guid userId, Guid followerId);
        Task UpdateUnfollowAsync(Guid userId, Guid unfollowUserId);

        Task AddAsync(Users user);
        Task<Users?> GetUserByIdAsync(Guid id);

        Task DeleteUserAsync(Guid id);
    }
}
