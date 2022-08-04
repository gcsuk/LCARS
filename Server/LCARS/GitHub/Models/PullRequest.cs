using Newtonsoft.Json;

namespace LCARS.GitHub.Models;

public record PullRequest
{
    [JsonProperty("number")]
    public int Number { get; set; }
    [JsonProperty("title")]
    public string? Title { get; set; }
    [JsonProperty("created_at")]
    public string? CreatedOn { get; set; }
    [JsonProperty("updated_at")]
    public string? UpdatedOn { get; set; }
    [JsonProperty("user")]
    public User User { get; set; } = new User();
    [JsonProperty("comments")]
    public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
}