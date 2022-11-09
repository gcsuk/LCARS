namespace LCARS.Configuration.Models
{
    public record TeamCitySettings
    {
        public bool Enabled { get; set; }
        public string? BaseUrl { get; set; } = "";
        public string? AccessToken { get; set; } = "";
        public IEnumerable<string>? BuildTypeIds { get; set; }
    }
}