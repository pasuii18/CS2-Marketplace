namespace CSMarketApp.Domain.Interfaces.ReposInterfaces
{
    public interface ITemplateRepo<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
        Task<T?> GetByName(string name);
        Task<int> GetOrCreate(T model);
        Task<int> Create(T record);
        void Delete(T record);
        Task SaveChanges();
    }
}