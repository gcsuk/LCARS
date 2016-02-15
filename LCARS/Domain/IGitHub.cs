using System.Collections.Generic;
using LCARS.ViewModels.GitHub;

namespace LCARS.Domain
{
    public interface IGitHub
    {
        ViewModels.GitHub.Settings GetSettings(string filePath);
        IEnumerable<Branch> GetBranches(string url, string repository);
        IEnumerable<PullRequest> GetPullRequests(string url, string repository);

        IEnumerable<string> GetComments(string url, string repository);
    }
}