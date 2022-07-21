namespace LCARS.Models.TeamCity;

public record BuildRunningResponse
{
    public int? Count { get; set; } = default;

    public IEnumerable<BuildRunningDetails> Build { get; set; } = Enumerable.Empty<BuildRunningDetails>();

    public record BuildRunningDetails
    {
        public int? Id { get; set; } = default;

        public string? Number { get; set; } = default;

        public string? BranchName { get; set; } = default;

        public string? Status { get; set; } = default;

        public string? State { get; set; } = default;

        public int? PercentageComplete { get; set; } = default;
    }
}