namespace LCARS.Models.Builds
{
    public class BuildProgress
    {
        public int Id { get; set; }
        public string BuildTypeId { get; set; }
        public string Status { get; set; }
        public string State { get; set; }
        public string PercentageComplete { get; set; }
        public string StatusText { get; set; }
    }
}
