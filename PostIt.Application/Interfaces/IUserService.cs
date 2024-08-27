using PostIt.Application.Dto;


namespace PostIt.Application.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(UserDto userDto);
    }
}
