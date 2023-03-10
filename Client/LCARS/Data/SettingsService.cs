using Microsoft.AspNetCore.Components;

namespace LCARS.Data;

public class SettingsService
{
    private readonly NavigationManager _navigationManager;

    public SettingsService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public async Task SelectScreen(Settings settings)
    {
        var screens = new List<ScreenPicker>();

        if (settings.RedAlertSettings?.Enabled ?? false && settings.RedAlertSettings?.EndTime > DateTime.UtcNow)
        {
            _navigationManager.NavigateTo("redalert");
            return;
        }

        if (settings.BitBucketSettings.Enabled)
        {
            screens.Add(new ScreenPicker("BitBucketPullRequests", "bitbucket/pullrequests"));
            screens.Add(new ScreenPicker("BitBucketBranches", "bitbucket/branches"));
        }

        if (settings.GitHubSettings.Enabled)
        {
            screens.Add(new ScreenPicker("GitHubPullRequests", "github/pullrequests"));
            screens.Add(new ScreenPicker("GitHubBranches", "github/branches"));
        }

        if (settings.TeamCitySettings.Enabled)
            screens.Add(new ScreenPicker("TeamCity", "builds"));

        if (settings.OctopusSettings.Enabled)
            screens.Add(new ScreenPicker("Octopus", "deployments"));

        if (settings.JiraSettings.Enabled)
            screens.Add(new ScreenPicker("Jira", "jira"));

        var selectedScreen = screens[new Random().Next(0, screens.Count)];

        _navigationManager.NavigateTo(selectedScreen.Path);
    }
}