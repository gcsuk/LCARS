using System.Text.Json.Serialization;

namespace LCARS.BitBucket.Models;

public record ApiAccessToken
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
    public string? Scopes { get; set; }
    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }
    [JsonPropertyName("expires_in")]
    public int? ExpiresIn { get; set; }
    public string? State { get; set; }
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }
}