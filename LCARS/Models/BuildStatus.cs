using System.Collections.Generic;

namespace LCARS.Models
{
    public class BuildStatus
    {
        public bool IsStatic { get; set; }

        public IEnumerable<Build> Builds { get; set; }
    }
}