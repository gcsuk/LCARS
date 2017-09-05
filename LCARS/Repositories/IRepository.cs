using System.Collections.Generic;
using System.Data;

namespace LCARS.Repositories
{
    public interface IRepository<T> where T : class
    {
        IDbConnection Connection { get; }
        int Add(T item);
        IEnumerable<T> GetAll();
        T GetByID(int id);
        void Update(T item);
        void Delete(int id);
    }
}
