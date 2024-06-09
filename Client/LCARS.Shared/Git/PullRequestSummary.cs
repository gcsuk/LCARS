namespace LCARS.Data;

public record PullRequestSummary
{
    public int Threshold { get; set; }

    public IEnumerable<PullRequestModel> PullRequests { get; set; } = Enumerable.Empty<PullRequestModel>();

    public record PullRequestModel
    {
        public string? Repository { get; set; }
        public int Number { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? State { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string? Author { get; set; }
        public int CommentCount { get; set; }
    }
}