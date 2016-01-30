using System;

namespace LCARS.ViewModels
{
    public class RedAlert
    {
        public bool IsEnabled { get; set; }

        public DateTime? TargetDate { get; set; }

        public string AlertType { get; set; }

        public override string ToString()
        {
            return IsEnabled + " " + AlertType + " " + TargetDate?.ToString("dd/MM/yyyy HH:mm");
        }
    }
}