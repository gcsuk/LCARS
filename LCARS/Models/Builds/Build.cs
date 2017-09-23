namespace LCARS.Models.Builds
{
    public class Build
    {
        public int Id { get; set; }
        public string BuildTypeId { get; set; }
        public string BuildTypeName { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }
        public string State { get; set; }
        public string PercentageComplete { get; set; } = "Unknown";
        public string StatusText { get; set; }
    }
}
