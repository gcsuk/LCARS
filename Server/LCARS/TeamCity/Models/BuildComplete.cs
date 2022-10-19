namespace LCARS.TeamCity.Models;

public record BuildComplete
{
    public int? Count { get; set; } = default;

    public IEnumerable<BuildDetails> Build { get; set; } = Enumerable.Empty<BuildDetails>();

    public record BuildDetails
    {
        public int? Id { get; set; } = default;

        public string? Number { get; set; } = default;

        public string? BranchName { get; set; } = default;

        public string? Status { get; set; } = default;

        public string? State { get; set; } = default;
    }
}