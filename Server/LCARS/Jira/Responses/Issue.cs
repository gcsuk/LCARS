namespace LCARS.Jira.Responses;

public record Issue
{
    public string? Name { get; set; } = default;
    public string? Description { get; set; } = default;
    public string? IssueType { get; set; } = default;
    public string? Status { get; set; } = default;
}