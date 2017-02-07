using System.Collections.Generic;
using System.Linq;

namespace LCARS.ViewModels.GitHub
{
    public class Repository
    {
        public Repository()
        {
            Branches = new List<Branch>();
            PullRequests = new List<PullRequest>();
        }

        public string Name { get; set; }

        public IEnumerable<Branch> Branches { get; set; }

        public IEnumerable<PullRequest> PullRequests { get; set; }

        public bool BranchCountWarning { get { return Branches.Count() > 100; } }

        public bool PullRequestUnshippedCountWarning { get { return PullRequests.Count(pr => !pr.IsShipped) > 6; } }

        public bool PullRequestShippedCountWarning { get { return PullRequests.Count(pr => pr.IsShipped) > 3; } }
    }
}