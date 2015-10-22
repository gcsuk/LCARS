using System.Collections.Generic;
using LCARS.Models;

namespace LCARS.ViewModels
{
    public class BuildStatus : RedAlert
    {
        public IEnumerable<Build> Builds { get; set; }

        public BuildSet BuildSet { get; set; }
    }
}