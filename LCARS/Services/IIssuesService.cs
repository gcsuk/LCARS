using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.ViewModels.Issues;

namespace LCARS.Services
{
    public interface IIssuesService
    {
        IEnumerable<Query> GetQueries(int? typeId = null);
        Task<IEnumerable<Issue>> GetIssues(string query);
    }
}