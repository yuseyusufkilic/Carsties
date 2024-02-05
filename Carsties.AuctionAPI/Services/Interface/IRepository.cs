using Carsties.AuctionAPI.Entities;

namespace Carsties.AuctionAPI.Services.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<int> Create(T entity);
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<int> Update(Guid id, T entity);
        Task<int> Delete(Guid id);
    }
}
