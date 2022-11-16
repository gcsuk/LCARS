using LCARS.Configuration.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LCARS.Configuration;

public class SettingsService : ISettingsService
{
    private readonly IBaseTableStorageRepository<SettingsEntity> _settingsRepository;

    public SettingsService(IBaseTableStorageRepository<SettingsEntity> settingsRepository)
    {
        _settingsRepository = settingsRepository;
    }

    public async Task<Settings> GetAllSettings() => new Settings
    {
        GitHubSettings = await GetGitHubSettings(),
        BitBucketSettings = await GetBitBucketSettings(),
        TeamCitySettings = await GetTeamCitySettings(),
        OctopusSettings = await GetOctopusSettings(),
        JiraSettings = await GetJiraSettings(),
        RedAlertSettings = await GetRedAlertSettings(),
    };

    public async Task<GitHubSettings> GetGitHubSettings()
    {
        var settings = await _settingsRepository.GetSingle("1", "GitHub");

        if (settings == null)
            return new GitHubSettings();

        return new GitHubSettings
        {
            BaseUrl = settings.BaseUrl,
            BranchThreshold = settings.BranchThreshold ?? 0,
            Enabled = settings.Enabled,
            Key = settings.AccessToken,
            Owner = settings.Owner,
            PullRequestThreshold = settings.PullRequestThreshold ?? 0,
            Repositories = settings.Repositories?.Split(",", StringSplitOptions.RemoveEmptyEntries),
        };
    }

    public async Task UpdateGitHubSettings(GitHubSettings settings)
    {
        if (settings == null)
            return;

        await _settingsRepository.Upsert(new SettingsEntity
        {
            PartitionKey = "1",
            RowKey = "GitHub",
            BaseUrl = settings.BaseUrl,
            BranchThreshold = settings.BranchThreshold,
            Enabled = settings.Enabled,
            AccessToken = settings.Key,
            Owner = settings.Owner,
            PullRequestThreshold = settings.PullRequestThreshold,
            Repositories = string.Join(",", settings.Repositories ?? Enumerable.Empty<string>()),
        });
    }

    public async Task<BitBucketSettings> GetBitBucketSettings()
    {
        var settings = await _settingsRepository.GetSingle("1", "BitBucket");

        if (settings == null)
            return new BitBucketSettings();

        return new BitBucketSettings
        {
            AuthUrl = settings.AuthUrl,
            BaseUrl = settings.BaseUrl ?? "",
            BranchThreshold = settings.BranchThreshold ?? 0,
            Enabled = settings.Enabled,
            Owner = settings.Owner,
            Password = settings.AccessToken,
            PullRequestThreshold = settings.PullRequestThreshold ?? 0,
            Repositories = settings.Repositories?.Split(",", StringSplitOptions.RemoveEmptyEntries),
            Username = settings.Username,
        };
    }

    public async Task UpdateBitBucketSettings(BitBucketSettings settings)
    {
        if (settings == null)
            return;

        await _settingsRepository.Upsert(new SettingsEntity
        {
            PartitionKey = "1",
            RowKey = "BitBucket",
            AuthUrl = settings.AuthUrl,
            BaseUrl = settings.BaseUrl,
            BranchThreshold = settings.BranchThreshold,
            Enabled = settings.Enabled,
            Owner = settings.Owner,
            AccessToken = settings.Password,
            PullRequestThreshold = settings.PullRequestThreshold,
            Repositories = string.Join(",", settings.Repositories ?? Enumerable.Empty<string>()),
            Username = settings.Username,
        });
    }

    public async Task<JiraSettings> GetJiraSettings()
    {
        var settings = await _settingsRepository.GetSingle("1", "Jira");

        if (settings == null)
            return new JiraSettings();

        return new JiraSettings
        {
            AccessToken = settings.AccessToken,
            BaseUrl = settings.BaseUrl,
            Enabled = settings.Enabled,
            Projects = settings.Projects?.Split(",", StringSplitOptions.RemoveEmptyEntries),
        };
    }

    public async Task UpdateJiraSettings(JiraSettings settings)
    {
        if (settings == null)
            return;

        await _settingsRepository.Upsert(new SettingsEntity
        {
            PartitionKey = "1",
            RowKey = "Jira",
            AccessToken = settings.AccessToken,
            BaseUrl = settings.BaseUrl,
            Enabled = settings.Enabled,
            Projects = string.Join(",", settings.Projects ?? Enumerable.Empty<string>()),
        });
    }

    public async Task<TeamCitySettings> GetTeamCitySettings()
    {
        var settings = await _settingsRepository.GetSingle("1", "TeamCity");

        if (settings == null)
            return new TeamCitySettings();

        return new TeamCitySettings
        {
            AccessToken = settings.AccessToken,
            BaseUrl = settings.BaseUrl ?? "",
            Builds = settings.Projects == null ? null : JsonSerializer.Deserialize<IEnumerable<TeamCitySettings.TeamCityBuild>>(settings.Projects),
            Enabled = settings.Enabled,
        };
    }

    public async Task UpdateTeamCitySettings(TeamCitySettings settings)
    {
        if (settings == null)
            return;

        await _settingsRepository.Upsert(new SettingsEntity
        {
            PartitionKey = "1",
            RowKey = "TeamCity",
            AccessToken = settings.AccessToken,
            BaseUrl = settings.BaseUrl,
            Projects = JsonSerializer.Serialize(settings.Builds ?? Enumerable.Empty<TeamCitySettings.TeamCityBuild>()),
            Enabled = settings.Enabled,
        });
    }

    public async Task<OctopusSettings> GetOctopusSettings()
    {
        var settings = await _settingsRepository.GetSingle("1", "Octopus");

        if (settings == null)
            return new OctopusSettings();

        return new OctopusSettings
        {
            Enabled = settings.Enabled,
            BaseUrl = settings.BaseUrl ?? "",
            ApiKey = settings.AccessToken,
        };
    }

    public async Task UpdateOctopusSettings(OctopusSettings settings)
    {
        if (settings == null)
            return;

        await _settingsRepository.Upsert(new SettingsEntity
        {
            PartitionKey = "1",
            RowKey = "Octopus",
            BaseUrl = settings.BaseUrl,
            AccessToken = settings.ApiKey,
            Enabled = settings.Enabled,
        });
    }

    public async Task<RedAlertSettings> GetRedAlertSettings()
    {
        var settings = await _settingsRepository.GetSingle("1", "RedAlert");

        if (settings == null)
            return new RedAlertSettings();

        return new RedAlertSettings
        {
            Enabled = settings.Enabled,
            AlertType = settings.AlertType,
            EndTime = settings.AlertEndTime
        };
    }

    public async Task UpdateRedAlertSettings(RedAlertSettings settings)
    {
        if (settings == null)
            return;

        await _settingsRepository.Upsert(new SettingsEntity
        {
            PartitionKey = "1",
            RowKey = "RedAlert",
            Enabled = settings.Enabled,
            AlertType = settings.AlertType,
            AlertEndTime = settings.EndTime == null ? null : new DateTime(settings.EndTime.Value.Ticks, DateTimeKind.Utc)
        });
    }
}