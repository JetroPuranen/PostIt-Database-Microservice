using PostIt.Application.Dto;


namespace PostIt.Application.Interfaces
{
    public interface IUnfollowService
    {
        Task RemoveFollowerAsync(UnfollowDto unfollowDto);
    }
}
