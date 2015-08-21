using System.Collections.Generic;

namespace LCARS.ViewModels
{
    public class BuildStatus
    {
        public IEnumerable<Models.Build> Builds { get; set; }

        public bool IsAutoDeployEnabled { get; set; }
    }
}