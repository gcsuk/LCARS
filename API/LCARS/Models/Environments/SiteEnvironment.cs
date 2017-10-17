namespace LCARS.Models.Environments
{
    public class SiteEnvironment
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
        public string SiteUrl { get; set; }
        public string PingUrl { get; set; }
        public string Status { get; set; }
        public string Version { get; set; }
    }
}