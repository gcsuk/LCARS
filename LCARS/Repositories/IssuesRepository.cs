using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using LCARS.Models.Issues;
using Microsoft.Extensions.Configuration;

namespace LCARS.Repositories
{
    public class IssuesRepository : IIssuesRepository
    {
        private readonly string _connectionString;

        public IssuesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection Connection => new SqlConnection(_connectionString);
   
        public int Add(Query settings)
        {
            using (var dbConnection = Connection)
            {
                const string query = "INSERT INTO IssueQueries" +
                                     "(Name, Deadline, Jql)" +
                                     "VALUES" +
                                     "(@Name, @Deadline, @Jql)";
                dbConnection.Open();
                return dbConnection.Execute(query, settings);
            }
        }

        public IEnumerable<Query> GetAll()
        {
            using (var dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Query>("SELECT * FROM IssueQueries");
            }
        }

        public Query GetByID(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "SELECT * FROM IssueQueries WHERE ID = @Id";
                dbConnection.Open();
                return dbConnection.Query<Query>(query, new { Id = id }).FirstOrDefault();
            }
        }

        public void Update(Query settings)
        {
            using(var dbConnection = Connection)
            {
                const string sql = "UPDATE IssueQueries" +
                                   "SET Name = @Name," +
                                   "Deadline = @Deadline," +
                                   "Jql = @Jql " +
                                   "WHERE ID = @Id";
                dbConnection.Open();
                dbConnection.Query(sql, settings);
            }
        }

        public void Delete(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "DELETE FROM IssueQueries WHERE ID = @Id";
                dbConnection.Open();
                dbConnection.Execute(query, new { Id = id });
            }
        }
    }
}
