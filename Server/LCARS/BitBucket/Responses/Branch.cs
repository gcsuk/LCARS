namespace LCARS.BitBucket.Responses;

public record Branch
{
    public string? Repository { get; set; }
    public string? BranchName { get; set; }
}