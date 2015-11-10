using System;

namespace LCARS.ViewModels.Issues
{
    public class Query
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Deadline { get; set; }

        public int DeadlineYear
        {
            get { return Deadline.HasValue ? Deadline.Value.Year : 0; }
        }

        public int DeadlineMonth
        {
            get { return Deadline.HasValue ? Deadline.Value.Month : 1; }
        }

        public int DeadlineDay
        {
            get { return Deadline.HasValue ? Deadline.Value.Day : 1; }
        }

        public int DeadlineHour
        {
            get { return Deadline.HasValue ? Deadline.Value.Hour : 0; }
        }

        public int DeadlineMinute
        {
            get { return Deadline.HasValue ? Deadline.Value.Minute : 0; }
        }

        public string Jql { get; set; }
    }
}