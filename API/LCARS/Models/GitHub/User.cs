using System.Text.Json.Serialization;

namespace LCARS.Models.GitHub
{
    public class User
    {
        [JsonPropertyName("login")]
        public string Name { get; set; }

        [JsonPropertyName("avatar_url")]
        public string Avatar { get; set; }
    }
}