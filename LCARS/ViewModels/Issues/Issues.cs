using System.Collections.Generic;

namespace LCARS.ViewModels.Issues
{
    public class Blockers : RedAlert
    {
        public IEnumerable<Issue> IssueList { get; set; }
    }
}