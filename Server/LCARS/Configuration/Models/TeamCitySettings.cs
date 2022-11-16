namespace LCARS.Configuration.Models
{
    public record TeamCitySettings
    {
        public bool Enabled { get; set; }
        public string? BaseUrl { get; set; } = "";
        public string? AccessToken { get; set; } = "";
        public IEnumerable<TeamCityBuild>? Builds { get; set; }

        public record TeamCityBuild
        {
            public string? DisplayName { get; set; }
            public string? BuildTypeId { get; set; }
        }
    }
}