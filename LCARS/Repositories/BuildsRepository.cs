using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LCARS.Models.Builds;
using System.Threading.Tasks;

namespace LCARS.Repositories
{
    public class BuildsRepository : IRepository<Settings>
    {
        private readonly IDbConnection _dbConnection;

        public BuildsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> Add(Settings settings)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "INSERT INTO Builds" +
                                     "(Id, ServerPassword, ServerUrl, ServerUsername) " +
                                     "VALUES" +
                                     "(@Id, @ServerPassword, @ServerUrl, @ServerUsername)";
                dbConnection.Open();
                return await dbConnection.ExecuteAsync(query, settings);
            }
        }

        public async Task<IEnumerable<Settings>> GetAll()
        {
            using (var dbConnection = _dbConnection)
            {
                dbConnection.Open();
                return await dbConnection.QueryAsync<Settings>("SELECT * FROM Builds");
            }
        }

        public async Task<Settings> GetByID(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "SELECT * FROM Builds WHERE Id = @Id";
                dbConnection.Open();
                return (await dbConnection.QueryAsync<Settings>(query, new { Id = id })).FirstOrDefault();
            }
        }

        public async Task Delete(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "DELETE FROM Builds WHERE ID = @Id";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task Update(Settings settings)
        {
            using (var dbConnection = _dbConnection)
            {
                const string sql = "UPDATE Builds " +
                                   "SET ServerPassword = @ServerPassword," +
                                   "    ServerUrl = @ServerUrl," +
                                   "    ServerUsername = @ServerUsername " +
                                   "WHERE Id = @Id";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(sql, settings);
            }
        }
    }
}