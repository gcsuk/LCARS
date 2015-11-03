using System;
using System.Collections.Generic;
using System.Linq;

namespace LCARS.ViewModels.Issues
{
    public class Bugs : RedAlertStatus
    {
        public IEnumerable<Issue> BugList { get; set; }

        public int Count => BugList.Count();

        public DateTime Deadline { get; set; }

        public int NumberOfWorkingDays
        {
            get
            {
                var dayCount = 0;
                var date = Deadline;

                while (date > DateTime.Now.Date)
                {
                    if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        dayCount++;
                    }

                    date = date.AddDays(-1);
                }

                return dayCount;
            }
        }

        public int NumberOfWorkingHours
        {
            get
            {
                var hourCount = NumberOfWorkingDays * 7.5M;

                return (int)Math.Floor(hourCount + (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 30, 0) - DateTime.Now).Hours);
            }
        }
    }
}