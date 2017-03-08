using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.ViewModels.GitHub;

namespace LCARS.Services
{
    public interface IGitHubService
    {
        Settings GetSettings();
        Task<IEnumerable<Branch>> GetBranches(string repository = null);
        Task<IEnumerable<PullRequest>> GetPullRequests(string repository = null);
        Task<IEnumerable<Comment>> GetComments(string repository = null, int pullRequestNumber = 0);
    }
}