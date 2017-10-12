using Newtonsoft.Json;
using System;

namespace LCARS.Models.GitHub
{
    public class Comment
    {
        [JsonProperty("created_at")]
        public DateTime DateCreated { get; set; }

        public User User { get; set; }

        public string Body { get; set; }
    }
}