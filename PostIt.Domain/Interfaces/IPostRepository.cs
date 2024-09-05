using PostIt.Domain.Entities;


namespace PostIt.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task AddAsync(Posts post);
    }
}
