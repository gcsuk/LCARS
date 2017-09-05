using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using LCARS.Models.Environments;
using Microsoft.Extensions.Configuration;

namespace LCARS.Repositories
{
    public class EnvironmentsRepository : IEnvironmentsRepository
    {
        private readonly string _connectionString;

        public EnvironmentsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public int Add(Site site)
        {
            using (var dbConnection = Connection)
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
                site.Id = dbConnection.Query<int>(siteQuery, site).Single();

                site.Environments.ToList().ForEach(e =>
                {
                    e.SiteId = site.Id;
                    dbConnection.Execute(environmentsQuery, e);
                });

                return site.Id;
            }
        }

        public void Delete(int id)
        {
            const string environmentsSelectSql = "SELECT * FROM Environments WHERE SiteId = @siteId";
            const string environmentsDeleteSql = "DELETE FROM Environments WHERE ID = @id";
            const string siteDeleteSql = "DELETE FROM Sites WHERE Id = @id; ";

            using (var dbConnection = Connection)
            {
                var environments = dbConnection.Query<Environment>(environmentsSelectSql, new { siteId = id });

                environments.ToList().ForEach(e =>
                {
                    dbConnection.Execute(environmentsDeleteSql, new {id = e.Id});
                });

                dbConnection.Execute(siteDeleteSql, new {id});
            }
        }

        public IEnumerable<Site> GetAll()
        {
            const string sql = @"SELECT * FROM Sites
                        SELECT * FROM Environments";

            using (var dbConnection = Connection)
            {
                var result = dbConnection.QueryMultiple(sql);
                var sites = result.Read<Site>().ToList();
                var environments = result.Read<Environment>().ToList();

                sites.ForEach(s =>
                {
                    s.Environments = environments.Where(e => e.SiteId == s.Id);
                });

                return sites;
            }
        }

        public Site GetByID(int id)
        {
            return GetAll().SingleOrDefault(s => s.Id == id);
        }

        public void Update(Site site)
        {
            using (var dbConnection = Connection)
            {
                const string siteQuery = "UPDATE Sites " +
                                         "SET Name = @Name " +
                                         "WHERE Id = @Id";

                const string environmentsQuery = "UPDATE Environments " +
                                                 "SET Name = @Name," +
                                                 "    Status = @Status " +
                                                 "WHERE Id = @Id";

                dbConnection.Open();
                dbConnection.Execute(siteQuery, site);

                site.Environments.ToList().ForEach(e =>
                {
                    e.SiteId = site.Id;
                    dbConnection.Execute(environmentsQuery, e);
                });
            }
        }
    }
}
