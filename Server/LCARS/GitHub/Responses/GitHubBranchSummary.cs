namespace LCARS.GitHub.Responses;

public record GitHubBranchSummary
{
    public string? Repository { get; set; }

    public List<GitHubBranchModel> Branches { get; set; } = new();

    public record GitHubBranchModel
    {
        public string? Name { get; set; }
    }
}