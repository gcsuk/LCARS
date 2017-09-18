using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LCARS.Models.Issues;
using System.Threading.Tasks;

namespace LCARS.Repositories
{
    public class IssueQueriesRepository : IRepository<Query>
    {
        private readonly IDbConnection _dbConnection;

        public IssueQueriesRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> Add(Query settings)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "INSERT INTO IssueQueries" +
                                     "(Name, Deadline, Jql)" +
                                     "VALUES" +
                                     "(@Name, @Deadline, @Jql)";
                dbConnection.Open();
                return await dbConnection.ExecuteAsync(query, settings);
            }
        }

        public async Task<IEnumerable<Query>> GetAll()
        {
            using (var dbConnection = _dbConnection)
            {
                dbConnection.Open();
                return await dbConnection.QueryAsync<Query>("SELECT * FROM IssueQueries");
            }
        }

        public async Task<Query> GetByID(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "SELECT * FROM IssueQueries WHERE ID = @Id";
                dbConnection.Open();
                var queries = await dbConnection.QueryAsync<Query>(query, new { Id = id });
                return queries.FirstOrDefault();
            }
        }

        public async Task Update(Query settings)
        {
            using(var dbConnection = _dbConnection)
            {
                const string sql = "UPDATE IssueQueries " +
                                   "SET Name = @Name," +
                                   "Deadline = @Deadline," +
                                   "Jql = @Jql " +
                                   "WHERE ID = @Id";
                dbConnection.Open();
                await dbConnection.QueryAsync(sql, settings);
            }
        }

        public async Task Delete(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "DELETE FROM IssueQueries WHERE ID = @Id";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
