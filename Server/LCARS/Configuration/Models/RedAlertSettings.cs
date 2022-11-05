namespace LCARS.Configuration.Models
{
    public record RedAlertSettings
    {
        public bool Enabled { get; set; }
        public string? AlertType { get; set; }
        public DateTime? EndTime { get; set; }
    }
}