using System.Collections.Generic;

namespace LCARS.ViewModels.GitHub
{
    public class Summary
    {
        public string Repository { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
        public IEnumerable<PullRequest> PullRequests { get; set; }
    }
}