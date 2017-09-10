using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.Models.Issues;

namespace LCARS.Services
{
    public interface IIssuesService
    {
        Task<IEnumerable<Query>> GetQueries(int? typeId = null);
        Task<IEnumerable<Issue>> GetIssues(string query);
        Task UpdateQuery(Query query);
        Task<Settings> GetSettings();
        Task UpdateSettings(Settings settings);
    }
}