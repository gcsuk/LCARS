using Newtonsoft.Json;

namespace LCARS.Models.GitHub;

public record ProjectResponse
{
    public int Count { get; set; }

    public IEnumerable<ProjectDetails> Project { get; set; } = Enumerable.Empty<ProjectDetails>();

    public record ProjectDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}