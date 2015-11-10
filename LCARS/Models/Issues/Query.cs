using System;

namespace LCARS.Models.Issues
{
    public class Query
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Deadline { get; set; }

        public string Jql { get; set; }
    }
}