using System.Text.Json.Serialization;

namespace LCARS.TeamCity.Models;

public record Project
{
    public int? Count { get; set; } = default;

    [JsonPropertyName("project")]
    public IEnumerable<ProjectDetails> ProjectData { get; set; } = Enumerable.Empty<ProjectDetails>();

    public record ProjectDetails
    {
        public string? Id { get; set; } = default;

        public string? Name { get; set; } = default;
    }
}