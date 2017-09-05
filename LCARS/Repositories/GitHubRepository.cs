using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using LCARS.Models.GitHub;
using Microsoft.Extensions.Configuration;

namespace LCARS.Repositories
{
    public class GitHubRepository : IGitHubRepository
    {
        private readonly string _connectionString;

        public GitHubRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public int Add(Settings settings)
        {
            using (var dbConnection = Connection)
            {
                const string query = "INSERT INTO GitHubSettings" +
                                     "(Id, BaseUrl, BranchThreshold, Owner, PullRequestThreshold, RepositoriesString) " +
                                     "VALUES" +
                                     "(@Id, @BaseUrl, @BranchThreshold, @Owner, @PullRequestThreshold, @RepositoriesString)";
                dbConnection.Open();
                return dbConnection.Execute(query, settings);
            }
        }

        public IEnumerable<Settings> GetAll()
        {
            using (var dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Settings>("SELECT * FROM GitHubSettings");
            }
        }

        public Settings GetByID(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "SELECT * FROM GitHubSettings WHERE ID = @Id";
                dbConnection.Open();
                return dbConnection.Query<Settings>(query, new { Id = id }).FirstOrDefault();
            }
        }

        public void Update(Settings settings)
        {
            using (var dbConnection = Connection)
            {
                const string sql = "UPDATE GitHubSettings" +
                                   "SET BaseUrl = @BaseUrl," +
                                   "    BranchThreshold = @BranchThreshold," +
                                   "    Owner = @Owner," +
                                   "    PullRequestThreshold = @PullRequestThreshold," +
                                   "    RepositoriesString = @RepositoriesString " +
                                   "WHERE ID = @Id";
                dbConnection.Open();
                dbConnection.Query(sql, settings);
            }
        }

        public void Delete(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "DELETE FROM GitHubSettings WHERE ID = @Id";
                dbConnection.Open();
                dbConnection.Execute(query, new { Id = id });
            }
        }
    }
}
