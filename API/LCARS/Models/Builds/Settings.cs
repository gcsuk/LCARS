namespace LCARS.Models.Builds
{
    public class Settings
    {
        public int Id { get; set; }
        public string ServerUrl { get; set; }
        public string ServerUsername { get; set; }
        public string ServerPassword { get; set; }
        public string ProjectIds { get; set; }
    }
}