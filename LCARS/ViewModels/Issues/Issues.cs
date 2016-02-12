using System.Collections.Generic;

namespace LCARS.ViewModels.Issues
{
    public class Issues : BaseSettings
    {
        public IEnumerable<Issue> IssueList { get; set; }
    }
}