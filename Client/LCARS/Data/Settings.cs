namespace LCARS.Data;

public record Settings
{
    public RedAlertSettingsModel RedAlertSettings { get; set; } = new();
    public GitHubSettingsModel GitHubSettings { get; set; } = new();
    public BitBucketSettingsModel BitBucketSettings { get; set; } = new();
    public TeamCitySettingsModel TeamCitySettings { get; set; } = new();
    public OctopusSettingsModel OctopusSettings { get; set; } = new();
    public JiraSettingsModel JiraSettings { get; set; } = new();

    public record RedAlertSettingsModel
    {
        public bool Enabled { get; set; }
        public string AlertType { get; set; } = "";
        public DateTime? EndTime { get; set; }
    }

    public record GitHubSettingsModel
    {
        public bool Enabled { get; set; }
        public int BranchThreshold { get; set; }
        public int PullRequestThreshold { get; set; }
    }

    public record BitBucketSettingsModel
    {
        public bool Enabled { get; set; }
        public int BranchThreshold { get; set; }
        public int PullRequestThreshold { get; set; }
    }

    public record JiraSettingsModel
    {
        public bool Enabled { get; set; }
    }

    public record TeamCitySettingsModel
    {
        public bool Enabled { get; set; }
    }

    public record OctopusSettingsModel
    {
        public bool Enabled { get; set; }
    }
}