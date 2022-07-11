namespace LCARS.Models.GitHub;

public record Settings
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Owner { get; set; }
    public string? BaseUrl { get; set; }
    public List<string> Repositories { get; set; } = new List<string>();
    public int BranchThreshold { get; set; }
    public int PullRequestThreshold { get; set; }
}