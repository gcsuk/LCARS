using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Azure;
using Azure.Data.Tables;

namespace LCARS.Configuration
{
    [ExcludeFromCodeCoverage]
    public class BaseTableStorageRepository<T> : IBaseTableStorageRepository<T> where T : class, ITableEntity, new()
    {
        private readonly TableClient _tableClient;

        public BaseTableStorageRepository(IConfiguration config, string tableName)
        {
            _tableClient = new TableClient(config.GetConnectionString("Settings"), tableName);
        }

        public async Task<IEnumerable<T>> GetList(string? partitionKey = null)
        {
            var entities = partitionKey != null
                            ? _tableClient.QueryAsync<T>(filter: $"PartitionKey eq '{partitionKey}'")
                            : _tableClient.QueryAsync<T>();

            var list = new List<T>();

            await foreach (T entity in entities)
            {
                list.Add(entity);
            }

            return list;
        }

        public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> query)
        {
            var entities = _tableClient.QueryAsync(query);

            var list = new List<T>();

            await foreach (T entity in entities)
            {
                list.Add(entity);
            }

            return list;
        }

        public async Task<T?> GetSingle(string partitionKey, string rowKey)
        {
            try
            {
                return await _tableClient.GetEntityAsync<T>(partitionKey, rowKey);
            }
            catch (RequestFailedException ex)
            {
                if (ex.Status == 404)
                    return null;

                throw;
            }
        }

        public async Task Upsert(T entity) => await _tableClient.UpsertEntityAsync(entity);

        public async Task Delete(string partitionKey, string rowKey) => await _tableClient.DeleteEntityAsync(partitionKey, rowKey);
    }
}