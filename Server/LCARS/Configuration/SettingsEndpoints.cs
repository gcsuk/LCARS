using LCARS.Configuration.Models;
using LCARS.Endpoints;

namespace LCARS.Configuration;

public class SettingsEndpoints : IEndpoints
{
    private const string Tag = "Settings";
    private const string BaseRoute = "settings";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}", GetAllSettings)
            .WithName("GetAllSettings")
            .Produces<IEnumerable<Settings>>(200)
            .WithTags(Tag);

        app.MapGet($"{BaseRoute}/redalert", GetRedAlertSettings)
            .WithName("GetRedAlertSettings")
            .Produces<RedAlertSettings>(200)
            .WithTags(Tag);

        app.MapPost($"{BaseRoute}/github", UpdateGitHubSettings)
            .WithName("UpdateGitHubSettings")
            .Produces(204)
            .WithTags(Tag);

        app.MapPost($"{BaseRoute}/bitbucket", UpdateBitBucketSettings)
            .WithName("UpdateBitBucketSettings")
            .Produces(204)
            .WithTags(Tag);

        app.MapPost($"{BaseRoute}/teamcity", UpdateTeamCitySettings)
            .WithName("UpdateTeamCitySettings")
            .Produces(204)
            .WithTags(Tag);

        app.MapPost($"{BaseRoute}/octopus", UpdateOctopusSettings)
            .WithName("UpdateOctopusSettings")
            .Produces(204)
            .WithTags(Tag);

        app.MapPost($"{BaseRoute}/jira", UpdateJiraSettings)
            .WithName("UpdateJiraSettings")
            .Produces(204)
            .WithTags(Tag);

        app.MapPost($"{BaseRoute}/redalert", UpdateRedAlertSettings)
            .WithName("UpdateRedAlertSettings")
            .Produces(204)
            .WithTags(Tag);
    }

    internal static async Task<IResult> GetAllSettings(ISettingsService settingsService) => Results.Ok(await settingsService.GetAllSettings());

    internal static async Task<IResult> GetRedAlertSettings(ISettingsService settingsService) => Results.Ok(await settingsService.GetRedAlertSettings());

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

    internal static async Task<IResult> UpdateOctopusSettings(ISettingsService settingsService, OctopusSettings settings)
    {
        await settingsService.UpdateOctopusSettings(settings);

        return Results.NoContent();
    }

    internal static async Task<IResult> UpdateJiraSettings(ISettingsService settingsService, JiraSettings settings)
    {
        await settingsService.UpdateJiraSettings(settings);

        return Results.NoContent();
    }

    internal static async Task<IResult> UpdateRedAlertSettings(ISettingsService settingsService, RedAlertSettings settings)
    {
        await settingsService.UpdateRedAlertSettings(settings);

        return Results.NoContent();
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBaseTableStorageRepository<SettingsEntity>, SettingsRepository>();
        services.AddScoped<ISettingsService, SettingsService>();
    }
}