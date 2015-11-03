using System.Collections.Generic;
using System.Linq;

namespace LCARS.ViewModels.Issues
{
    public class Bugs : RedAlertStatus
    {
        public IEnumerable<Issue> BugList { get; set; }

        public int Count => BugList.Count();
    }
}