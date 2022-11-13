using LCARS.Data;
using LCARS.Pages;
using Refit;

namespace LCARS.Services;

public interface IApiClient
{
    [Get("/bitbucket/branches")]
    Task<IEnumerable<BranchSummary.RepositoryModel>> GetBitBucketBranches();

    [Get("/bitbucket/pullrequests")]
    Task<IEnumerable<PullRequestSummary.PullRequestModel>> GetBitBucketPullRequests();

    [Get("/github/branches")]
    Task<IEnumerable<BranchSummary.RepositoryModel>> GetGitHubBranches();

    [Get("/github/pullrequests")]
    Task<IEnumerable<PullRequestSummary.PullRequestModel>> GetGitHubPullRequests();

    [Get("/jira/issues")]
    Task<IEnumerable<Issue>> GetJiraIssues();

    [Get("/teamcity/projects")]
    Task<IEnumerable<Project>> GetTeamCityProjects();

    [Get("/teamcity/builds")]
    Task<IEnumerable<Build>> GetTeamCityBuilds();

    [Get("/octopus")]
    Task<IEnumerable<ProjectDeployment>> GetOctopusDeployments();

    [Get("/settings")]
    Task<Settings> GetAllSettings();

    [Post("/settings/github")]
    Task UpdateGitHubSettings(Settings.GitHubSettingsModel settings);

    [Post("/settings/bitbucket")]
    Task UpdateBitBucketSettings(Settings.BitBucketSettingsModel settings);

    [Post("/settings/teamcity")]
    Task UpdateTeamCitySettings(Settings.TeamCitySettingsModel settings);

    [Post("/settings/octopus")]
    Task UpdateOctopusSettings(Settings.OctopusSettingsModel settings);

    [Post("/settings/redalert")]
    Task UpdateRedAlertSettings(Settings.RedAlertSettingsModel settings);
}