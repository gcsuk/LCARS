using System.Collections.Generic;
using LCARS.ViewModels.Builds;

namespace LCARS.Domain
{
    public interface IBuilds
    {
        IEnumerable<BuildProject> GetBuilds(string path);

        IEnumerable<Build> GetBuildStatus(IEnumerable<string> buildTypeIds);
    }
}