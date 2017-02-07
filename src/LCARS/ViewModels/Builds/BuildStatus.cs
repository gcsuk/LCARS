using System.Collections.Generic;

namespace LCARS.ViewModels.Builds
{
    public class BuildStatus
    {
        public int ProjectId { get; set; }

        public IEnumerable<Build> Builds { get; set; }
    }
}