using System;

namespace LCARS.ViewModels.Issues
{
    public class Query
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Deadline { get; set; }

        public int DeadlineYear => Deadline?.Year ?? 0;

        public int DeadlineMonth => Deadline?.Month ?? 1;

        public int DeadlineDay => Deadline?.Day ?? 1;

        public int DeadlineHour => Deadline?.Hour ?? 0;

        public int DeadlineMinute => Deadline?.Minute ?? 0;

        public string Jql { get; set; }
    }
}