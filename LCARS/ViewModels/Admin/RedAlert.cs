namespace LCARS.ViewModels.Admin
{
    public class RedAlert
    {
        public bool IsEnabled { get; set; }

        public int TargetYear { get; set; }

        public int TargetMonth { get; set; }

        public int TargetDay { get; set; }

        public int TargetHour { get; set; }

        public int TargetMinute { get; set; }

        public string AlertType { get; set; }
    }
}