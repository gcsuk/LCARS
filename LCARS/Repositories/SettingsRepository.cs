using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using LCARS.Models;
using Microsoft.Extensions.Configuration;

namespace LCARS.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly string _connectionString;

        public SettingsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public int Add(Settings settings)
        {
            using (var dbConnection = Connection)
            {
                const string query = "INSERT INTO Settings" +
                                     "(BuildServerPassword, BuildServerUrl, BuildServerUsername," +
                                     "DeploymentsServerKey, DeploymentsServerUrl," +
                                     "GitHubPassword, GitHubUsername," +
                                     "IssuesPassword, IssuesUrl, IssuesUsername) " +
                                     "VALUES" +
                                     "(@BuildServerPassword, @BuildServerUrl, @BuildServerUsername," +
                                     "@DeploymentsServerKey, @DeploymentsServerUrl," +
                                     "@GitHubPassword, @GitHubUsername," +
                                     "@IssuesPassword, @IssuesUrl, @IssuesUsername)";
                dbConnection.Open();
                return dbConnection.Execute(query, settings);
            }
        }

        public IEnumerable<Settings> GetAll()
        {
            using (var dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Settings>("SELECT * FROM Settings");
            }
        }

        public Settings GetByID(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "SELECT * FROM Settings WHERE ID = @Id";
                dbConnection.Open();
                return dbConnection.Query<Settings>(query, new { Id = id }).FirstOrDefault();
            }
        }

        public void Delete(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "DELETE FROM Settings WHERE ID = @Id";
                dbConnection.Open();
                dbConnection.Execute(query, new { Id = id });
            }
        }

        public void Update(Settings settings)
        {
            using (var dbConnection = Connection)
            {
                const string sql = "UPDATE Settings" +
                                   "SET BuildServerPassword = @BuildServerPassword," +
                                   "BuildServerUrl = @BuildServerUrl," +
                                   "BuildServerUsername = @BuildServerUsername," +
                                   "DeploymentsServerKey = @DeploymentsServerKey," +
                                   "DeploymentsServerUrl = @DeploymentsServerUrl," +
                                   "GitHubPassword = @GitHubPassword," +
                                   "GitHubUsername = @GitHubUsername," +
                                   "IssuesPassword = @IssuesPassword," +
                                   "IssuesUrl = @IssuesUrl," +
                                   "IssuesUsername = @IssuesUsername " +
                                   "WHERE ID = @Id";
                dbConnection.Open();
                dbConnection.Query(sql, settings);
            }
        }
    }
}