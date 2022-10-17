using System.Text.Json.Serialization;

namespace LCARS.GitHub.Models;

public record PullRequest
{
    public int Number { get; set; }
    public string? Title { get; set; }
    [JsonPropertyName("created_at")]
    public string? CreatedOn { get; set; }
    [JsonPropertyName("updated_at")]
    public string? UpdatedOn { get; set; }
    public User User { get; set; } = new User();
    public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
}