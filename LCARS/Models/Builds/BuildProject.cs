using System.Collections.Generic;

namespace LCARS.Models.Builds
{
    public class BuildProject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Build> Builds { get; set; }
    }
}