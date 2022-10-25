using LCARS.Configuration;
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

    internal static async Task<IResult> GetPullRequests(IGitHubService gitHubService, ISettingsService settingsService)
    {
        var settings = await settingsService.GetGitHubSettings();

        var pullRequests = await gitHubService.GetPullRequests(settings, true);

        return Results.Ok(pullRequests);
    }

    internal static async Task<IResult> GetBranches(IGitHubService gitHubService, ISettingsService settingsService)
    {
        var settings = await settingsService.GetGitHubSettings();

        var branches = await gitHubService.GetBranches(settings);

        return Results.Ok(branches);
    }

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