using System.Collections.Generic;

namespace LCARS.ViewModels.Builds
{
    public class BuildProject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Build> Builds { get; set; }
    }
}