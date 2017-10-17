using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LCARS.Models.Environments;
using System.Threading.Tasks;

namespace LCARS.Repositories
{
    public class EnvironmentsRepository : IRepository<SiteEnvironment>
    {
        private readonly IDbConnection _dbConnection;

        public EnvironmentsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> Add(SiteEnvironment environment)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "INSERT INTO SitesEnvironments" +
                                     "(SiteId, Name, SiteUrl, PingUrl) " +
                                     "VALUES" +
                                     "(@SiteId, @Name, @SiteUrl, @PingUrl);" +
                                     " SELECT CAST(SCOPE_IDENTITY() AS INT)";

                dbConnection.Open();

                environment.Id = (await dbConnection.QueryAsync<int>(query, environment)).Single();

                return environment.Id;
            }
        }

        public async Task Delete(int id)
        {
            const string query = "DELETE FROM SitesEnvironments WHERE Id = @id";

            using (var dbConnection = _dbConnection)
            {
                await dbConnection.ExecuteAsync(query, new {id});
            }
        }

        public async Task<IEnumerable<SiteEnvironment>> GetAll()
        {
            using (var dbConnection = _dbConnection)
            {
                return await dbConnection.QueryAsync<SiteEnvironment>("SELECT * FROM SitesEnvironments");
            }
        }

        public async Task<SiteEnvironment> GetByID(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "SELECT * FROM SitesEnvironments WHERE Id = @Id";
                dbConnection.Open();
                var settings = await dbConnection.QueryAsync<SiteEnvironment>(query, new {Id = id});
                return settings.FirstOrDefault();
            }
        }

        public async Task Update(SiteEnvironment site)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "UPDATE SitesEnvironments " +
                                     "SET   Name = @Name," +
                                     "      SiteUrl = @SiteUrl," +
                                     "      PingUrl = @PingUrl " +
                                     "WHERE Id = @Id";

                dbConnection.Open();
                await dbConnection.ExecuteAsync(query, site);
            }
        }
    }
}
