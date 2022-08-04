namespace LCARS.TeamCity.Models;

public record ProjectResponse
{
    public int? Count { get; set; } = default;

    public IEnumerable<ProjectDetails> Project { get; set; } = Enumerable.Empty<ProjectDetails>();

    public record ProjectDetails
    {
        public string? Id { get; set; } = default;

        public string? Name { get; set; } = default;
    }
}