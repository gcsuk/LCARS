using System.Text.Json.Serialization;

namespace LCARS.BitBucket.Models;

public record Branches
{
    public IEnumerable<BranchModel>? Values { get; set; }

    public record BranchModel
    {
        public string? Name { get; set; }
        public TargetModel Target { get; set; } = new TargetModel();

        public record TargetModel
        {
            public DateTime? Date { get; set; }
            public AuthorModel? Author { get; set; } = new AuthorModel();

            public record AuthorModel
            {
                public UserModel User { get; set; } = new UserModel();

                public record UserModel
                {
                    [JsonPropertyName("nickname")]
                    public string? Name { get; set; }
                }
            }
        }
    }
}