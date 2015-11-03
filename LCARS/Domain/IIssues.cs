using System.Collections.Generic;

namespace LCARS.Domain
{
    public interface IIssues
    {
        IEnumerable<ViewModels.Issues.Query> GetQueries(string path);

        IEnumerable<ViewModels.Issues.Issue> Get(string query);
    }
}