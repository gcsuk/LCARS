namespace LCARS.Configuration.Models
{
    public record OctopusSettings
    {
        public bool Enabled { get; set; }
        public string? BaseUrl { get; set; }
        public string? ApiKey { get; set; }
    }
}