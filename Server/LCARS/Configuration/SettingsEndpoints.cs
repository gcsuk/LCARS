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
            .Produces<Settings>(200)
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

    internal static async Task<Settings> GetAllSettings(ISettingsService settingsService) => await settingsService.GetAllSettings();

    internal static async Task<RedAlertSettings> GetRedAlertSettings(ISettingsService settingsService) => await settingsService.GetRedAlertSettings();

    internal static async Task UpdateGitHubSettings(ISettingsService settingsService, GitHubSettings settings) => await settingsService.UpdateGitHubSettings(settings);

    internal static async Task UpdateBitBucketSettings(ISettingsService settingsService, BitBucketSettings settings) => await settingsService.UpdateBitBucketSettings(settings);

    internal static async Task UpdateTeamCitySettings(ISettingsService settingsService, TeamCitySettings settings) => await settingsService.UpdateTeamCitySettings(settings);

    internal static async Task UpdateOctopusSettings(ISettingsService settingsService, OctopusSettings settings) => await settingsService.UpdateOctopusSettings(settings);

    internal static async Task UpdateJiraSettings(ISettingsService settingsService, JiraSettings settings) => await settingsService.UpdateJiraSettings(settings);

    internal static async Task UpdateRedAlertSettings(ISettingsService settingsService, RedAlertSettings settings) => await settingsService.UpdateRedAlertSettings(settings);

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBaseTableStorageRepository<SettingsEntity>, SettingsRepository>();
        services.AddScoped<ISettingsService, SettingsService>();
    }
}