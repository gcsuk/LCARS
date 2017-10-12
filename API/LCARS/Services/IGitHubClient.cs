using System.Threading.Tasks;
using Refit;
using System.Collections.Generic;

namespace LCARS.Services
{
    public interface IGitHubClient<T>
    {
        [Get("/{type}?per_page=100&page={pageNumber}")]
        Task<IEnumerable<T>> GetBranches([Header("User-Agent")] string agent, [Header("Authorization")] string token, string type, int pageNumber);

        [Get("/{type}?per_page=100&page={pageNumber}")]
        Task<IEnumerable<T>> GetPullRequests([Header("User-Agent")] string agent, [Header("Authorization")] string token, string type, int pageNumber);

        [Get("/pulls/{type}/comments?per_page=100&page={pageNumber}")]
        Task<IEnumerable<T>> GetComments([Header("User-Agent")] string agent, [Header("Authorization")] string token, string type, int pageNumber);

    }
}