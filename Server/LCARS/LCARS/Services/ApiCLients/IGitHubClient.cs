using Refit;

namespace LCARS.Services.ApiClients;

[Headers(new[] { "User-Agent: LCARS" })]
public interface IGitHubClient
{
    [Get("/repos/{owner}/{repository}/{type}?per_page=100&page={pageNumber}")]
    Task<IEnumerable<T>> GetData<T>([Header("Authorization")] string token, string owner, string repository, string type, int pageNumber);
}