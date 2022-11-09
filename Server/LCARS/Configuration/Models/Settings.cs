namespace LCARS.Configuration.Models;

public record Settings
{
    public GitHubSettings? GitHubSettings { get; set; }
    public BitBucketSettings? BitBucketSettings { get; set; }
    public TeamCitySettings? TeamCitySettings { get; set; }
    public OctopusSettings? OctopusSettings { get; set; }
    public JiraSettings? JiraSettings { get; set; }
    public RedAlertSettings? RedAlertSettings { get; set; }
}