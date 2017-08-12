using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.ViewModels.Builds;

namespace LCARS.Services
{
    public interface IBuildsService
    {
        Task<Dictionary<string, int>> GetBuildsRunning();

        Task<BuildProgress> GetBuildProgress(int buildId);

        Task<KeyValuePair<string, string>> GetLastBuildStatus(string buildTypeId);
    }
}