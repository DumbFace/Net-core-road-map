using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repository.Base
{
    public interface IRepository<T, out TContext> where T : class where TContext : DbContext
    {
        DbSet<T> Entity { get; }
        TContext Context { get; }
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
