namespace LCARS.Data;

public record BranchSummary
{
    public int Threshold { get; set; }

    public IEnumerable<RepositoryModel> Repositories { get; set; } = Enumerable.Empty<RepositoryModel>();

    public record RepositoryModel
    {
        public string? Repository { get; set; }

        public List<BranchModel> Branches { get; set; } = new();

        public record BranchModel
        {
            public string? Name { get; set; }
        }
    }
}