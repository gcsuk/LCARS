using System.Text.Json.Serialization;

namespace LCARS.GitHub.Models;

public record PullRequest
{
    public int Number { get; set; }
    public string? Title { get; set; }
    [JsonPropertyName("body")]
    public string? Description{ get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedOn { get; set; }
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedOn { get; set; }
    public string? State { get; set; }
    public UserModel User { get; set; } = new UserModel();

    public record UserModel
    {
        [JsonPropertyName("login")]
        public string? Name { get; set; }

        [JsonPropertyName("avatar_url")]
        public string? Avatar { get; set; }
    }
}