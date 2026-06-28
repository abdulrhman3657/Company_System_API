

namespace Company_System_Infrastructure.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> Get();
        void Add(T item);
        T? GetById(int id);
        T? Edit(int id, T updatedItem);
        bool Delete(int id);
    }
}
