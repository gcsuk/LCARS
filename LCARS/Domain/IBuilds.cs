using System.Collections.Generic;
using LCARS.Models;

namespace LCARS.Domain
{
    public interface IBuilds
    {
        IEnumerable<Build> GetBuilds(string path);

        IEnumerable<Build> GetBuildStatus(IEnumerable<int> buildTypeIds);
    }
}