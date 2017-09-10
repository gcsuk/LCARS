using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LCARS.Models.Environments;
using System.Threading.Tasks;

namespace LCARS.Repositories
{
    public class EnvironmentsRepository : IEnvironmentsRepository
    {
        private readonly IDbConnection _dbConnection;

        public EnvironmentsRepository(IDbConnection dbConnection)
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

                const string environmentsQuery = "INSERT INTO Environments" +
                                                 "(SiteId, Name, Status) " +
                                                 "VALUES" +
                                                 "(@SiteId, @Name, @Status)";

                dbConnection.Open();
                site.Id = (await dbConnection.QueryAsync<int>(siteQuery, site)).Single();

                site.Environments.ToList().ForEach(async e =>
                {
                    e.SiteId = site.Id;
                    await dbConnection.ExecuteAsync(environmentsQuery, e);
                });

                return site.Id;
            }
        }

        public async Task Delete(int id)
        {
            const string environmentsSelectSql = "SELECT * FROM Environments WHERE SiteId = @siteId";
            const string environmentsDeleteSql = "DELETE FROM Environments WHERE ID = @id";
            const string siteDeleteSql = "DELETE FROM Sites WHERE Id = @id; ";

            using (var dbConnection = _dbConnection)
            {
                var environments = await dbConnection.QueryAsync<Environment>(environmentsSelectSql, new { siteId = id });

                environments.ToList().ForEach(async e =>
                {
                    await dbConnection.ExecuteAsync(environmentsDeleteSql, new {id = e.Id});
                });

                await dbConnection.ExecuteAsync(siteDeleteSql, new {id});
            }
        }

        public async Task<IEnumerable<Site>> GetAll()
        {
            const string sql = @"SELECT * FROM Sites
                        SELECT * FROM Environments";

            using (var dbConnection = _dbConnection)
            {
                var result = await dbConnection.QueryMultipleAsync(sql);
                var sites = result.Read<Site>().ToList();
                var environments = result.Read<Environment>().ToList();

                sites.ForEach(s =>
                {
                    s.Environments = environments.Where(e => e.SiteId == s.Id);
                });

                return sites;
            }
        }

        public async Task<Site> GetByID(int id)
        {
            return (await GetAll()).SingleOrDefault(s => s.Id == id);
        }

        public async Task Update(Site site)
        {
            using (var dbConnection = _dbConnection)
            {
                const string siteQuery = "UPDATE Sites " +
                                         "SET Name = @Name " +
                                         "WHERE Id = @Id";

                const string environmentsQuery = "UPDATE Environments " +
                                                 "SET Name = @Name," +
                                                 "    Status = @Status " +
                                                 "WHERE Id = @Id";

                dbConnection.Open();
                await dbConnection.ExecuteAsync(siteQuery, site);

                site.Environments.ToList().ForEach(async e =>
                {
                    e.SiteId = site.Id;
                    await dbConnection.ExecuteAsync(environmentsQuery, e);
                });
            }
        }
    }
}
