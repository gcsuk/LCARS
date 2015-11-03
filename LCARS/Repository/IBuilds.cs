using System.Collections.Generic;
using LCARS.Models;

namespace LCARS.Repository
{
    public interface IBuilds
    {
        Dictionary<int, int> GetBuildsRunning();

        BuildProgress GetBuildProgress(int buildId);

        KeyValuePair<string, string> GetLastBuildStatus(int buildTypeId);
    }
}