using System;

namespace LCARS.Models.AlertCondition
{
    public class AlertCondition
    {
        public int Id { get; set; }
        public string Condition { get; set; }
        public string AlertType { get; set; }
        public DateTime EndDate { get; set; }
    }
}