using PostIt.Application.Dto;


namespace PostIt.Application.Interfaces
{
    public interface IFollowerService
    {
        Task AddFollowerAsync(FollowerDto followerDto);
    }
}
