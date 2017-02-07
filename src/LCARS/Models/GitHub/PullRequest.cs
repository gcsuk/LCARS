using System;
using Newtonsoft.Json;

namespace LCARS.Models.GitHub
{
    public class PullRequest
    {
        public int Number { get; set; }

        public string Title { get; set; }

        [JsonProperty("Created_At")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("Updated_At")]
        public DateTime UpdatedOn { get; set; }

        public User User { get; set; }
    }
}