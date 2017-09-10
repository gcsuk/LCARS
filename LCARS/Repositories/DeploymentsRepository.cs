using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LCARS.Models.Deployments;
using System.Threading.Tasks;

namespace LCARS.Repositories
{
    public class DeploymentsRepository : IDeploymentsRepository
    {
        private readonly IDbConnection _dbConnection;

        public DeploymentsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> Add(Settings settings)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "INSERT INTO Deployments " +
                                     "(Id, ServerKey, ServerUrl) " +
                                     "VALUES" +
                                     "(@Id, @ServerKey, @ServerUrl)";
                dbConnection.Open();
                return await dbConnection.ExecuteAsync(query, settings);
            }
        }

        public async Task<IEnumerable<Settings>> GetAll()
        {
            using (var dbConnection = _dbConnection)
            {
                dbConnection.Open();
                return await dbConnection.QueryAsync<Settings>("SELECT * FROM Deployments");
            }
        }

        public async Task<Settings> GetByID(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "SELECT * FROM Deployments WHERE Id = @Id";
                dbConnection.Open();
                return (await dbConnection.QueryAsync<Settings>(query, new { Id = id })).FirstOrDefault();
            }
        }

        public async Task Delete(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "DELETE FROM Deployments WHERE Id = @Id";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task Update(Settings settings)
        {
            using (var dbConnection = _dbConnection)
            {
                const string sql = "UPDATE Deployments " +
                                   "SET ServerKey = @ServerKey," +
                                   "    ServerUrl = @ServerUrl " +
                                   "WHERE Id = @Id";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(sql, settings);
            }
        }
    }
}