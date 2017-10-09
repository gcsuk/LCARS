using System;

namespace LCARS.Models.Issues
{
    public class Fields
    {
        public string Summary { get; set; }

        public DateTime Created { get; set; }

        public Priority Priority { get; set; }

        public Person Reporter { get; set; }

        public Person Assignee { get; set; }

        public Status Status { get; set; }
    }
}