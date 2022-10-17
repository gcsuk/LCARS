using System.Text.Json.Serialization;

namespace LCARS.GitHub.Models;

public class User
{
    [JsonPropertyName("login")]
    public string? Name { get; set; }

    [JsonPropertyName("avatar_url")]
    public string? Avatar { get; set; }
}