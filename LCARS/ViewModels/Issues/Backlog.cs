using System;
using System.Collections.Generic;
using System.Linq;

namespace LCARS.ViewModels.Issues
{
    public class Backlog : RedAlertStatus
    {
        public string IssueSet { get; set; }

        public IEnumerable<Issue> BugList { get; set; }

        public int Count => BugList.Count();

        public DateTime? Deadline { get; set; }

        public int NumberOfWorkingDays
        {
            get
            {
                if (!Deadline.HasValue)
                {
                    return 0;
                }

                var dayCount = 0;
                var date = Deadline;

                while (date > DateTime.Now.Date)
                {
                    if (date.Value.DayOfWeek != DayOfWeek.Saturday && date.Value.DayOfWeek != DayOfWeek.Sunday)
                    {
                        dayCount++;
                    }

                    date = date.Value.AddDays(-1);
                }

                return dayCount;
            }
        }

        public int NumberOfWorkingHours
        {
            get
            {
                if (!Deadline.HasValue)
                {
                    return 0;
                }

                var hourCount = NumberOfWorkingDays * 7.5M;

                // If it is after 17:30, just return the full days remaining
                if (DateTime.Now.Hour >= 18 || (DateTime.Now.Hour == 17 && DateTime.Now.Minute >= 30))
                {
                    return (int)Math.Floor(hourCount);
                }

                return (int)Math.Floor(hourCount + (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 30, 0) - DateTime.Now).Hours);
            }
        }

        public int NumberOfWorkingMinutes
        {
            get
            {
                if (!Deadline.HasValue)
                {
                    return 0;
                }

                if (DateTime.Now.Hour >= 18 || (DateTime.Now.Hour == 17 && DateTime.Now.Minute >= 30))
                {
                    return 30;
                }

                if (DateTime.Now.Minute > 30)
                {
                    return (60 - DateTime.Now.Minute) + 30;
                }

                return 30 - DateTime.Now.Minute;
            }
        }
    }
}