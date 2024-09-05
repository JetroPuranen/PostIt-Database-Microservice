using PostIt.Application.Dto;


namespace PostIt.Application.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(UserDto userDto);
        Task<UserDetailDto?> GetUserByIdAsync(Guid id);

        Task DeleteUserAsync(Guid id);
    }
}
