using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.ViewModels.GitHub;

namespace LCARS.Services
{
    public interface IGitHubService
    {
        Settings GetSettings();
        Task<IEnumerable<Branch>> GetBranches(string repository);
        Task<IEnumerable<PullRequest>> GetPullRequests(string repository);
        Task<IEnumerable<Comment>> GetComments(string repository, int pullRequestNumber);
    }
}