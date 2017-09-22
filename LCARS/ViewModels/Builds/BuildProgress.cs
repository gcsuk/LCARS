namespace LCARS.ViewModels.Builds
{
    public class BuildProgress
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
        public string State { get; set; }
        public string PercentageComplete { get; set; }
    }
}