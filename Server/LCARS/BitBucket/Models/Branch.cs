namespace LCARS.BitBucket.Models;

public record Branches
{
    public IEnumerable<BranchModel>? Values { get; set; }

    public record BranchModel
    {
        public string? Name { get; set; }
    }
}