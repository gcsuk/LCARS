using LCARS.Configuration.Models;

namespace LCARS.Configuration;

public interface ISettingsService
{
    Task<Settings> GetAllSettings();
    Task<BitBucketSettings> GetBitBucketSettings();
    Task<GitHubSettings> GetGitHubSettings();
    Task<JiraSettings> GetJiraSettings();
    Task<TeamCitySettings> GetTeamCitySettings();
    Task UpdateBitBucketSettings(BitBucketSettings settings);
    Task UpdateGitHubSettings(GitHubSettings settings);
    Task UpdateJiraSettings(JiraSettings settings);
    Task UpdateTeamCitySettings(TeamCitySettings settings);
}