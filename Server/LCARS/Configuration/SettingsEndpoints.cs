using LCARS.Configuration.Models;
using LCARS.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LCARS.Configuration;

public class SettingsEndpoints : IEndpoints
{
    private const string Tag = "Settings";
    private const string BaseRoute = "settings";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}", GetAllSettings).WithTags(Tag);
        app.MapGet($"{BaseRoute}/redalert", GetRedAlertSettings).WithTags(Tag);
        app.MapPost($"{BaseRoute}/github", UpdateGitHubSettings).WithTags(Tag);
        app.MapPost($"{BaseRoute}/bitbucket", UpdateBitBucketSettings).WithTags(Tag);
        app.MapPost($"{BaseRoute}/teamcity", UpdateTeamCitySettings).WithTags(Tag);
        app.MapPost($"{BaseRoute}/octopus", UpdateOctopusSettings).WithTags(Tag);
        app.MapPost($"{BaseRoute}/jira", UpdateJiraSettings).WithTags(Tag);
        app.MapPost($"{BaseRoute}/redalert", UpdateRedAlertSettings).WithTags(Tag);
    }

    internal static async Task<Ok<Settings>> GetAllSettings(ISettingsService settingsService)
        => TypedResults.Ok(await settingsService.GetAllSettings());

    internal static async Task<Ok<RedAlertSettings>> GetRedAlertSettings(ISettingsService settingsService)
        => TypedResults.Ok(await settingsService.GetRedAlertSettings());

    internal static async Task<Results<NoContent, BadRequest<string>>> UpdateGitHubSettings(ISettingsService settingsService, GitHubSettings settings)
    {
        await settingsService.UpdateGitHubSettings(settings);
        return TypedResults.NoContent();
    }

    internal static async Task<Results<NoContent, BadRequest<string>>> UpdateBitBucketSettings(ISettingsService settingsService, BitBucketSettings settings)
    {
        if (settings is null)
            return TypedResults.BadRequest("Invalid configuration");

        await settingsService.UpdateBitBucketSettings(settings);
        return TypedResults.NoContent();
    }

    internal static async Task<Results<NoContent, BadRequest<string>>> UpdateTeamCitySettings(ISettingsService settingsService, TeamCitySettings settings)
    {
        if (settings is null)
            return TypedResults.BadRequest("Invalid configuration");

        await settingsService.UpdateTeamCitySettings(settings);
        return TypedResults.NoContent();
    }

    internal static async Task<Results<NoContent, BadRequest<string>>> UpdateOctopusSettings(ISettingsService settingsService, OctopusSettings settings)
    {
        if (settings is null)
            return TypedResults.BadRequest("Invalid configuration");

        await settingsService.UpdateOctopusSettings(settings);
        return TypedResults.NoContent();
    }

    internal static async Task<Results<NoContent, BadRequest<string>>> UpdateJiraSettings(ISettingsService settingsService, JiraSettings settings)
    {
        if (settings is null)
            return TypedResults.BadRequest("Invalid configuration");

        await settingsService.UpdateJiraSettings(settings);
        return TypedResults.NoContent();
    }

    internal static async Task<Results<NoContent, BadRequest<string>>> UpdateRedAlertSettings(ISettingsService settingsService, RedAlertSettings settings)
    {
        if (settings is null)
            return TypedResults.BadRequest("Invalid configuration");

        await settingsService.UpdateRedAlertSettings(settings);
        return TypedResults.NoContent();
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBaseTableStorageRepository<SettingsEntity>, SettingsRepository>();
        services.AddScoped<ISettingsService, SettingsService>();
    }
}