using System.Collections.Generic;

namespace LCARS.Repository
{
    public interface IRepository<T>
    {
        T Get(string filePath);

        IEnumerable<T> GetList(string filePath);

        void Update(string filePath, T settings);

        void UpdateList(string filePath, IEnumerable<T> settings);
    }
}