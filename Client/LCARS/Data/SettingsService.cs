using LCARS.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Components;

namespace LCARS.Data;

public class SettingsService
{
    private readonly IApiClient _apiClient;
    private readonly IMemoryCache _memoryCache;
    private readonly NavigationManager _navigationManager;

    public SettingsService(IApiClient apiClient, IMemoryCache memoryCache, NavigationManager navigationManager)
    {
        _apiClient = apiClient;
        _memoryCache = memoryCache;
        _navigationManager = navigationManager;
    }

    public async Task SelectScreen()
    {
        var settings = await GetSettings();

        var screens = new List<ScreenPicker>();

        if (settings.RedAlertSettings?.Enabled ?? false && settings.RedAlertSettings?.EndTime > DateTime.UtcNow)
        {
            _navigationManager.NavigateTo("/redalert");
            return;
        }

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
            screens.Add(new ScreenPicker("TeamCity", "/builds"));

        if (settings.OctopusSettings.Enabled)
            screens.Add(new ScreenPicker("Octopus", "/deployments"));

        if (settings.JiraSettings.Enabled)
            screens.Add(new ScreenPicker("Jira", "/jira"));

        var selectedScreen = screens[new Random().Next(0, screens.Count)];

        _navigationManager.NavigateTo(selectedScreen.Path);
    }

    public async Task<Settings> GetSettings()
    {
        var success = _memoryCache.TryGetValue("Settings", out Settings settings);

        if (!success)
        {
            settings = await _apiClient.GetAllSettings();

            _memoryCache.Set("Settings", settings, DateTime.Now.AddMinutes(60));
        }

        return settings;
    }

    public void ClearSettingsCache()
    {
        _memoryCache.Remove("Settings");
    }

    public async Task UpdateRedAlertSettings(Settings.RedAlertSettingsModel settings)
    {
        await _apiClient.UpdateRedAlertSettings(settings);
        ClearSettingsCache();
    }
}