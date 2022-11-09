using Azure.Data.Tables;
using System.Linq.Expressions;

namespace LCARS.Configuration
{
    public interface IBaseTableStorageRepository<T> where T : class, ITableEntity, new()
    {
        Task Delete(string partitionKey, string rowKey);
        Task<IEnumerable<T>> GetList(string? partitionKey = null);
        Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> query);
        Task<T?> GetSingle(string partitionKey, string rowKey);
        Task Upsert(T entity);
    }
}