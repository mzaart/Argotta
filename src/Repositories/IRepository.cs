using System;
using System.Linq;
using System.Threading.Tasks;

namespace Multilang.Repositories
{
    public interface IRepository<T>
    {
        Task Insert(T entity);
        Task Delete(T entity);
        Task Save();
        Task<T> Find(Predicate<T> predicate);
        IQueryable<T> GetAll();
        Task<T> GetById(string id);
    }
}