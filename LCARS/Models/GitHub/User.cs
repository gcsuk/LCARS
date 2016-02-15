using Newtonsoft.Json;

namespace LCARS.Models.GitHub
{
    public class User
    {
        [JsonProperty("login")]
        public string Name { get; set; }

        [JsonProperty("avatar_url")]
        public string Avatar { get; set; }
    }
}