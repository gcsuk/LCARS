namespace LCARS.Configuration.Models;

public record GitHubSettings
{
    public bool Enabled { get; set; }
    public string? BaseUrl { get; set; }
    public string? Key { get; set; }
    public string? Owner { get; set; }
    public IEnumerable<string>? Repositories { get; set; }
    public int BranchThreshold { get; set; }
    public int PullRequestThreshold { get; set; }
}