namespace LCARS.Data;

public record Settings
{
    public GitHubSettingsModel GitHubSettings { get; set; } = new GitHubSettingsModel();
    public BitBucketSettingsModel BitBucketSettings { get; set; } = new BitBucketSettingsModel();
    public TeamCitySettingsModel TeamCitySettings { get; set; } = new TeamCitySettingsModel();
    public JiraSettingsModel JiraSettings { get; set; } = new JiraSettingsModel();

    public record GitHubSettingsModel
    {
        public bool Enabled { get; set; }
    }

    public record BitBucketSettingsModel
    {
        public bool Enabled { get; set; }
    }

    public record JiraSettingsModel
    {
        public bool Enabled { get; set; }
    }

    public record TeamCitySettingsModel
    {
        public bool Enabled { get; set; }
    }
}