using LCARS.Configuration.Models;
using LCARS.Endpoints;

namespace LCARS.Configuration;

public class SettingsEndpoints : IEndpoints
{
    private const string Tag = "Settings";
    private const string BaseRoute = "settings";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}/settings", GetAllSettings)
            .WithName("GetSettings")
            .Produces<IEnumerable<Settings>>(200)
            .WithTags(Tag);

        app.MapPost($"{BaseRoute}/settings/github", UpdateGitHubSettings)
            .WithName("UpdateGitHubSettings")
            .Produces(204)
            .WithTags(Tag);

        app.MapPost($"{BaseRoute}/settings/bitbucket", UpdateBitBucketSettings)
            .WithName("UpdateBitBucketSettings")
            .Produces(204)
            .WithTags(Tag);

        app.MapPost($"{BaseRoute}/settings/teamcity", UpdateTeamCitySettings)
            .WithName("UpdateTeamCitySettings")
            .Produces(204)
            .WithTags(Tag);

        app.MapPost($"{BaseRoute}/settings/jira", UpdateJiraSettings)
            .WithName("UpdateJiraSettings")
            .Produces(204)
            .WithTags(Tag);
    }

    internal static async Task<IResult> GetAllSettings(ISettingsService settingsService) => Results.Ok(await settingsService.GetAllSettings());

    internal static async Task<IResult> UpdateGitHubSettings(ISettingsService settingsService, GitHubSettings settings)
    {
        await settingsService.UpdateGitHubSettings(settings);

        return Results.NoContent();
    }

    internal static async Task<IResult> UpdateBitBucketSettings(ISettingsService settingsService, BitBucketSettings settings)
    {
        await settingsService.UpdateBitBucketSettings(settings);

        return Results.NoContent();
    }

    internal static async Task<IResult> UpdateTeamCitySettings(ISettingsService settingsService, TeamCitySettings settings)
    {
        await settingsService.UpdateTeamCitySettings(settings);

        return Results.NoContent();
    }

    internal static async Task<IResult> UpdateJiraSettings(ISettingsService settingsService, JiraSettings settings)
    {
        await settingsService.UpdateJiraSettings(settings);

        return Results.NoContent();
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBaseTableStorageRepository<SettingsEntity>, SettingsRepository>();
        services.AddScoped<ISettingsService, SettingsService>();
    }
}