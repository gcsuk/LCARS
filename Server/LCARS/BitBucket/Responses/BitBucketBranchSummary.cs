namespace LCARS.BitBucket.Responses;

public record BitBucketBranchSummary
{
    public string? Repository { get; set; }

    public List<BitBucketBranchModel> Branches { get; set; } = new();

    public record BitBucketBranchModel
    {
        public string? Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? User { get; set; }
    }
}