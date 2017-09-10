using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LCARS.Models.Issues;
using System.Threading.Tasks;

namespace LCARS.Repositories
{
    public class IssueSettingsRepository : IIssueSettingsRepository
    {
        private readonly IDbConnection _dbConnection;

        public IssueSettingsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> Add(Settings settings)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "INSERT INTO IssueSettings" +
                                     "(Id, Username, Password, Url) " +
                                     "VALUES" +
                                     "(@Id, @Username, @Password, @Url)";
                dbConnection.Open();
                return await dbConnection.ExecuteAsync(query, settings);
            }
        }

        public async Task<IEnumerable<Settings>> GetAll()
        {
            using (var dbConnection = _dbConnection)
            {
                dbConnection.Open();
                return await dbConnection.QueryAsync<Settings>("SELECT * FROM IssueSettings");
            }
        }

        public async Task<Settings> GetByID(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "SELECT * FROM IssueSettings WHERE Id = @Id";
                dbConnection.Open();
                var settings = await dbConnection.QueryAsync<Settings>(query, new { Id = id });
                return settings.FirstOrDefault();
            }
        }

        public async Task Delete(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "DELETE FROM IssueSettings WHERE Id = @Id";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task Update(Settings settings)
        {
            using (var dbConnection = _dbConnection)
            {
                const string sql = "UPDATE IssueSettings " +
                                   "SET Username = @Username," +
                                   "    Password = @Password," +
                                   "    Url = @Url " +
                                   "WHERE Id = @Id";
                dbConnection.Open();
                await dbConnection.QueryAsync(sql, settings);
            }
        }
    }
}