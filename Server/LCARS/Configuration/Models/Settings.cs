namespace LCARS.Configuration.Models;

public record Settings
{
    public GitHubSettings GitHubSettings { get; set; } = new GitHubSettings();
    public BitBucketSettings BitBucketSettings { get; set; } = new BitBucketSettings();
    public TeamCitySettings TeamCitySettings { get; set; } = new TeamCitySettings();
    public JiraSettings JiraSettings { get; set; } = new JiraSettings();
}