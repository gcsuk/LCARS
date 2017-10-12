using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LCARS.Models.GitHub;
using System.Threading.Tasks;

namespace LCARS.Repositories
{
    public class GitHubRepository : IRepository<Settings>
    {
        private readonly IDbConnection _dbConnection;

        public GitHubRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> Add(Settings settings)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "INSERT INTO GitHubSettings " +
                                     "(Id, Username, Password, BaseUrl, BranchThreshold, Owner, PullRequestThreshold, RepositoriesString) " +
                                     "VALUES " +
                                     "(@Id, @Username, @Password, @BaseUrl, @BranchThreshold, @Owner, @PullRequestThreshold, @RepositoriesString)";
                dbConnection.Open();
                return await dbConnection.ExecuteAsync(query, settings);
            }
        }

        public async Task<IEnumerable<Settings>> GetAll()
        {
            using (var dbConnection = _dbConnection)
            {
                dbConnection.Open();
                return await dbConnection.QueryAsync<Settings>("SELECT * FROM GitHubSettings");
            }
        }

        public async Task<Settings> GetByID(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "SELECT * FROM GitHubSettings WHERE ID = @Id";
                dbConnection.Open();
                var settings = await dbConnection.QueryAsync<Settings>(query, new { Id = id });
                return settings.FirstOrDefault();
            }
        }

        public async Task Update(Settings settings)
        {
            using (var dbConnection = _dbConnection)
            {
                const string sql = "UPDATE GitHubSettings " +
                                   "SET Username = @Username," +
                                   "    Password = @Password," +
                                   "    BaseUrl = @BaseUrl," +
                                   "    BranchThreshold = @BranchThreshold," +
                                   "    Owner = @Owner," +
                                   "    PullRequestThreshold = @PullRequestThreshold," +
                                   "    RepositoriesString = @RepositoriesString " +
                                   "WHERE ID = @Id";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(sql, settings);
            }
        }

        public async Task Delete(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "DELETE FROM GitHubSettings WHERE ID = @Id";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
