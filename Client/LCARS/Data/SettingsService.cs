using LCARS.Services;

namespace LCARS.Data;

public class SettingsService
{
    private readonly IApiClient _apiClient;

    public SettingsService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<ScreenPicker> GetScreen()
    {
        var settings = await _apiClient.GetAllSettings();

        var screens = new List<ScreenPicker>();

        if (settings.BitBucketSettings.Enabled)
        {
            screens.Add(new ScreenPicker("BitBucketPullRequests", "/bitbucket/pullrequests"));
            //screens.Add(new ScreenPicker("BitBucketBranches", "/bitbucket/branches"));
        }

        if (settings.GitHubSettings.Enabled)
        {
            screens.Add(new ScreenPicker("GitHubPullRequests", "/github/pullrequests"));
            //screens.Add(new ScreenPicker("GitHubBranches", "/github/branches"));
        }

        if (settings.TeamCitySettings.Enabled)
            screens.Add(new ScreenPicker("TeamCity", "/teamcity"));

        if (settings.JiraSettings.Enabled)
            screens.Add(new ScreenPicker("Jira", "/jira"));

        return screens[new Random().Next(0, screens.Count - 1)];
    }
}