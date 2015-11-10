using System.Collections.Generic;

namespace LCARS.ViewModels.Issues
{
    public class Issues : RedAlertStatus
    {
        public IEnumerable<Issue> IssueList { get; set; }
    }
}