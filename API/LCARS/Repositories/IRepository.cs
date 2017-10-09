using System.Collections.Generic;
using System.Threading.Tasks;

namespace LCARS.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<int> Add(T item);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByID(int id);
        Task Update(T item);
        Task Delete(int id);
    }
}
