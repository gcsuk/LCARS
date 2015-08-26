using System.Collections.Generic;
using LCARS.Models;

namespace LCARS.ViewModels
{
    public class BuildStatus
    {
        public IEnumerable<Build> Builds { get; set; }

        public bool IsAutoDeployEnabled { get; set; }
    }
}