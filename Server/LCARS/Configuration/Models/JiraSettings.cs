namespace LCARS.Configuration.Models
{
    public record JiraSettings
    {
        public bool Enabled { get; set; }
        public string? BaseUrl { get; set; } = "";
        public string? AccessToken { get; set; } = "";
        public IEnumerable<string>? Projects { get; set; }
    }
}