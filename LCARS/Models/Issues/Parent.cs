using System.Collections.Generic;

namespace LCARS.Models.Issues
{
    public class Parent
    {
        public int StartAt { get; set; }

        public int MaxResults { get; set; }

        public int Total { get; set; }

        public IEnumerable<Issue> Issues { get; set; }
    }
}