using System.Collections.Generic;
using LCARS.Models.Builds;

namespace LCARS.ViewModels
{
    public class BuildStatus : BaseSettings
    {
        public int ProjectId { get; set; }

        public IEnumerable<Build> Builds { get; set; }
    }
}