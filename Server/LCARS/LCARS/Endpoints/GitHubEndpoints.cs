using LCARS.Endpoints.Internal;
using LCARS.Models.GitHub;
using LCARS.Services;

namespace LCARS.Endpoints;

public class GitHubEndpoints : IEndpoints
{
    private const string Tag = "GitHub";
    private const string BaseRoute = "github";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}/pullrequests", GetPullRequests)
            .WithName("GetPullRequests")
            .Produces<IEnumerable<PullRequest>>(200)
            .WithTags(Tag);

        app.MapGet($"{BaseRoute}/branches", GetBranches)
            .WithName("GetBranches")
            .Produces<IEnumerable<Branch>>(200)
            .WithTags(Tag);
    }

    internal static async Task<IResult> GetPullRequests(IGitHubService gitHubService) => Results.Ok(await gitHubService.GetPullRequests(false));

    internal static async Task<IResult> GetBranches(IGitHubService gitHubService) => Results.Ok(await gitHubService.GetBranches());

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IGitHubService, GitHubService>();
    }
}