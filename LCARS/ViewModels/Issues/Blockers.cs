using System.Collections.Generic;

namespace LCARS.ViewModels.Issues
{
    public class Blockers : RedAlertStatus
    {
        public IEnumerable<Issue> IssueList { get; set; }
    }
}