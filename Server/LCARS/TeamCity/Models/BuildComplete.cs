namespace LCARS.TeamCity.Models;

public record BuildComplete
{
    public int? Count { get; set; }

    public IEnumerable<BuildDetails> Build { get; set; } = Enumerable.Empty<BuildDetails>();

    public record BuildDetails
    {
        public string? BuildTypeId { get; set; }

        public string? Number { get; set; }

        public string? BranchName { get; set; }

        public string? Status { get; set; }

        public string? State { get; set; }

        public string? WebUrl { get; set; }
    }
}