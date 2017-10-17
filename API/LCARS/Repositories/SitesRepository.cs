using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LCARS.Models.Environments;
using System.Threading.Tasks;

namespace LCARS.Repositories
{
    public class SitesRepository : IRepository<Site>
    {
        private readonly IDbConnection _dbConnection;

        public SitesRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> Add(Site site)
        {
            using (var dbConnection = _dbConnection)
            {
                const string siteQuery = "INSERT INTO Sites" +
                                         "(Name) " +
                                         "VALUES" +
                                         "(@Name);" +
                                         " SELECT CAST(SCOPE_IDENTITY() AS INT)";

                dbConnection.Open();

                site.Id = (await dbConnection.QueryAsync<int>(siteQuery, site)).Single();

                return site.Id;
            }
        }

        public async Task Delete(int id)
        {
            const string query = "DELETE FROM Sites WHERE Id = @id; ";

            using (var dbConnection = _dbConnection)
            {
                await dbConnection.ExecuteAsync(query, new {id});
            }
        }

        public async Task<IEnumerable<Site>> GetAll()
        {
            using (var dbConnection = _dbConnection)
            {
                return await dbConnection.QueryAsync<Site>("SELECT * FROM Sites");
            }
        }

        public async Task<Site> GetByID(int id)
        {
            using (var dbConnection = _dbConnection)
            {
                const string query = "SELECT * FROM Sites WHERE ID = @Id";
                dbConnection.Open();
                var settings = await dbConnection.QueryAsync<Site>(query, new { Id = id });
                return settings.FirstOrDefault();
            }
        }

        public async Task Update(Site site)
        {
            using (var dbConnection = _dbConnection)
            {
                const string siteQuery = "UPDATE Sites " +
                                         "SET Name = @Name " +
                                         "WHERE Id = @Id";

                dbConnection.Open();
                await dbConnection.ExecuteAsync(siteQuery, site);
            }
        }
    }
}
