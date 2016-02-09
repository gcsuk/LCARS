using System.Collections.Generic;
using LCARS.Models.Builds;

namespace LCARS.Services
{
    public interface IBuilds
    {
        Dictionary<string, int> GetBuildsRunning();

        BuildProgress GetBuildProgress(int buildId);

        KeyValuePair<string, string> GetLastBuildStatus(string buildTypeId);
    }
}