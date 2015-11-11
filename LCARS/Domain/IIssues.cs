using System.Collections.Generic;

namespace LCARS.Domain
{
    public interface IIssues
    {
        IEnumerable<ViewModels.Issues.Query> GetQueries(string path);

        bool UpdateQuery(string filePath, ViewModels.Issues.Query query);

        void DeleteQuery(string filePath, int id);

        IEnumerable<ViewModels.Issues.Issue> Get(string query);
    }
}