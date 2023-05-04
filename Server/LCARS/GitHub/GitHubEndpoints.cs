using LCARS.Configuration;
using LCARS.Endpoints;
using LCARS.GitHub.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Refit;

namespace LCARS.GitHub;

public class GitHubEndpoints : IEndpoints
{
    private const string Tag = "GitHub";
    private const string BaseRoute = "github";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}/pullrequests", GetPullRequests).WithTags(Tag);
        app.MapGet($"{BaseRoute}/branches", GetBranches).WithTags(Tag);
    }

    internal static async Task<Ok<IEnumerable<GitHubPullRequest>>> GetPullRequests(IGitHubService gitHubService, ISettingsService settingsService)
        => TypedResults.Ok(await gitHubService.GetPullRequests());

    internal static async Task<Ok<IEnumerable<GitHubBranchSummary>>> GetBranches(IGitHubService gitHubService, ISettingsService settingsService) 
        => TypedResults.Ok(await gitHubService.GetBranches());

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration["GitHub:BaseUrl"];

        if (string.IsNullOrEmpty(baseUrl))
            return;

        services.AddRefitClient<IGitHubClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        if (Convert.ToBoolean(configuration["EnableMocks"]))
            services.AddScoped<IGitHubService, MockGitHubService>();
        else
            services.AddScoped<IGitHubService, GitHubService>();
    }
}