using LCARS.Data;
using Refit;

namespace LCARS.Services;

public interface IApiClient
{
    [Get("/bitbucket/branches")]
    Task<IEnumerable<Branch>> GetBitBucketBranches();

    [Get("/bitbucket/pullrequests")]
    Task<IEnumerable<PullRequest>> GetBitBucketPullRequests();

    [Get("/github/branches")]
    Task<IEnumerable<Branch>> GetGitHubBranches();

    [Get("/github/pullrequests")]
    Task<IEnumerable<PullRequest>> GetGitHubPullRequests();

    [Get("/jira/issues")]
    Task<IEnumerable<Issue>> GetJiraIssues();

    [Get("/teamcity/projects")]
    Task<IEnumerable<Project>> GetTeamCityProjects();

    [Get("/teamcity/builds/complete")]
    Task<IEnumerable<Build>> GetTeamCityBuildsComplete();

    [Get("/teamcity/builds/running")]
    Task<IEnumerable<Build>> GetTeamCityBuildsRunning();
}