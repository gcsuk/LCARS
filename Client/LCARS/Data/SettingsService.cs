using LCARS.Services;
using Microsoft.Extensions.Caching.Memory;

namespace LCARS.Data;

public class SettingsService
{
    private readonly IApiClient _apiClient;
    private readonly IMemoryCache _memoryCache;

    public SettingsService(IApiClient apiClient, IMemoryCache memoryCache)
    {
        _apiClient = apiClient;
        _memoryCache = memoryCache;
    }

    public async Task<ScreenPicker> GetScreen()
    {
        var success = _memoryCache.TryGetValue("Settings", out Settings settings);

        if (!success)
        {
            settings = await _apiClient.GetAllSettings();

            _memoryCache.Set("Settings", settings, DateTime.Now.AddMinutes(60));
        }

        var screens = new List<ScreenPicker>();

        if (settings.BitBucketSettings.Enabled)
        {
            screens.Add(new ScreenPicker("BitBucketPullRequests", "/bitbucket/pullrequests"));
            screens.Add(new ScreenPicker("BitBucketBranches", "/bitbucket/branches"));
        }

        if (settings.GitHubSettings.Enabled)
        {
            screens.Add(new ScreenPicker("GitHubPullRequests", "/github/pullrequests"));
            screens.Add(new ScreenPicker("GitHubBranches", "/github/branches"));
        }

        if (settings.TeamCitySettings.Enabled)
            screens.Add(new ScreenPicker("TeamCity", "/teamcity"));

        if (settings.JiraSettings.Enabled)
            screens.Add(new ScreenPicker("Jira", "/jira"));

        return screens[new Random().Next(0, screens.Count - 1)];
    }
}