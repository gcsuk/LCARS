using LCARS.Endpoints;
using LCARS.GitHub.Models;
using Refit;

namespace LCARS.GitHub;

public class GitHubEndpoints : IEndpoints
{
    private const string Tag = "GitHub";
    private const string BaseRoute = "github";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}/pullrequests", GetPullRequests)
            .WithName("GetGitHubPullRequests")
            .Produces<IEnumerable<PullRequest>>(200)
            .WithTags(Tag);

        app.MapGet($"{BaseRoute}/branches", GetBranches)
            .WithName("GetGitHubBranches")
            .Produces<IEnumerable<Branch>>(200)
            .WithTags(Tag);
    }

    internal static async Task<IResult> GetPullRequests(IGitHubService gitHubService) => Results.Ok(await gitHubService.GetPullRequests(true));

    internal static async Task<IResult> GetBranches(IGitHubService gitHubService) => Results.Ok(await gitHubService.GetBranches());

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration["TeamCity:BaseUrl"];

        if (string.IsNullOrEmpty(baseUrl))
            return;

        services.AddRefitClient<IGitHubClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        if (Convert.ToBoolean(configuration["EnableMocks"]))
            services.AddScoped<IGitHubService, MockGitHubService>();
        else
            services.AddScoped<IGitHubService, GitHubService>();
    }
}