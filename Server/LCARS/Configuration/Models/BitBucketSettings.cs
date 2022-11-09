namespace LCARS.Configuration.Models;

public record BitBucketSettings
{
    public bool Enabled { get; set; }
    public string? AuthUrl { get; set; }
    public string? BaseUrl { get; set; }
    public string? Owner { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public IEnumerable<string>? Repositories { get; set; }
    public int BranchThreshold { get; set; }
    public int PullRequestThreshold { get; set; }
}