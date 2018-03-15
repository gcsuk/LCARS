using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LCARS.Models.AlertCondition;
using System.Threading.Tasks;

namespace LCARS.Repositories
{
    public class AlertConditionRepository : IRepository<AlertCondition>
    {
        private readonly IDbConnection _dbConnection;

        public AlertConditionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> Add(AlertCondition settings)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AlertCondition>> GetAll()
        {
            using (var dbConnection = _dbConnection)
            {
                dbConnection.Open();
                return await dbConnection.QueryAsync<AlertCondition>("SELECT * FROM AlertCondition");
            }
        }

        public async Task<AlertCondition> GetByID(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "SELECT * FROM AlertCondition WHERE ID = @Id";
                dbConnection.Open();
                var settings = await dbConnection.QueryAsync<AlertCondition>(query, new { Id = id });
                return settings.FirstOrDefault();
            }
        }

        public async Task Update(AlertCondition settings)
        {
            using (var dbConnection = _dbConnection)
            {
                const string sql = "UPDATE AlertCondition " +
                                   "SET Condition = @Condition," +
                                   "    AlertType = @AlertType," +
                                   "    EndDate = @EndDate " +
                                   "WHERE ID = @Id";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(sql, settings);
            }
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
