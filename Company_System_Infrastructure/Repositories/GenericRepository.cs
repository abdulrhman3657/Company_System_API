using Company_System_Infrastructure.Data;

namespace Company_System_Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DB db;

        public GenericRepository(DB dbFromDI)
        {
            db = dbFromDI;
        }

        public List<T> Get()
        {
            return db.Set<T>().ToList();
        }

        public void Add(T item)
        {
            db.Set<T>().Add(item);

            db.SaveChanges();
        }

        public T? GetById(int id)
        {
            T? item = db.Set<T>().Find(id);

            return item;
        }
    }
}
