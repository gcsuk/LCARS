using System.Collections.Generic;

namespace LCARS.Domain
{
    public interface IIssues
    {
        IEnumerable<ViewModels.Issues.Issue> Get();
    }
}