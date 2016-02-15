using System.Collections.Generic;

namespace LCARS.ViewModels.GitHub
{
    public class Summary
    {
        public Summary()
        {
            Repositories = new List<Repository>();
            PullRequests = new List<PullRequest>();
        }

        public List<Repository> Repositories { get; set; }

        public List<PullRequest> PullRequests { get; set; }
    }
}