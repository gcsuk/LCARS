using LCARS.Configuration.Models;

namespace LCARS.Configuration;

public interface ISettingsService
{
    Task<Settings> GetAllSettings();
    Task<BitBucketSettings> GetBitBucketSettings();
    Task<GitHubSettings> GetGitHubSettings();
    Task<TeamCitySettings> GetTeamCitySettings();
    Task<OctopusSettings> GetOctopusSettings();
    Task<JiraSettings> GetJiraSettings();
    Task<RedAlertSettings> GetRedAlertSettings();
    Task UpdateBitBucketSettings(BitBucketSettings settings);
    Task UpdateGitHubSettings(GitHubSettings settings);
    Task UpdateTeamCitySettings(TeamCitySettings settings);
    Task UpdateOctopusSettings(OctopusSettings settings);
    Task UpdateJiraSettings(JiraSettings settings);
    Task UpdateRedAlertSettings(RedAlertSettings settings);
}