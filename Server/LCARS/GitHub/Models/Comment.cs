using Newtonsoft.Json;

namespace LCARS.GitHub.Models;

public record Comment
{
    [JsonProperty("created_at")]
    public DateTime DateCreated { get; set; }
    public User User { get; set; } = new User();
    public string? Body { get; set; }
}