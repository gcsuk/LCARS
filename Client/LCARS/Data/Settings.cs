namespace LCARS.Data;

public record Settings
{
    public RedAlertSettingsModel RedAlertSettings { get; set; } = new RedAlertSettingsModel();
    public GitHubSettingsModel GitHubSettings { get; set; } = new GitHubSettingsModel();
    public BitBucketSettingsModel BitBucketSettings { get; set; } = new BitBucketSettingsModel();
    public TeamCitySettingsModel TeamCitySettings { get; set; } = new TeamCitySettingsModel();
    public JiraSettingsModel JiraSettings { get; set; } = new JiraSettingsModel();

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
}