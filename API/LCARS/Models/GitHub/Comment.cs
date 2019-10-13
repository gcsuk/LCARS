using System;
using System.Text.Json.Serialization;

namespace LCARS.Models.GitHub
{
    public class Comment
    {
        [JsonPropertyName("created_at")]
        public DateTime DateCreated { get; set; }

        public User User { get; set; }

        public string Body { get; set; }
    }
}