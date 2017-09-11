using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.Models.GitHub;

namespace LCARS.Services
{
    public interface IGitHubService
    {
        Task<IEnumerable<Branch>> GetBranches(string repository = null);
        Task<IEnumerable<PullRequest>> GetPullRequests(string repository = null, bool includeCommeents = false);
        Task<IEnumerable<Comment>> GetComments(string repository = null, int pullRequestNumber = 0);
        Task<Settings> GetSettings();
        Task UpdateSettings(Settings settings);
    }
}