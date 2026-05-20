

namespace Company_System_API.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> Get();
        void Add(T item);
        T? GetById(int id);
    }
}
