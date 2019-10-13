using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LCARS.Models.GitHub
{
    public class PullRequest
    {
        public int Number { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("Created_At")]
        public DateTime CreatedOn { get; set; }
        [JsonPropertyName("Updated_At")]
        public DateTime UpdatedOn { get; set; }
        public User User { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}