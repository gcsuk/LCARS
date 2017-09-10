using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.Models.Builds;

namespace LCARS.Services
{
    public interface IBuildsService
    {
        Task<Dictionary<string, int>> GetBuildsRunning();
        Task<BuildProgress> GetBuildProgress(int buildId);
        Task<KeyValuePair<string, string>> GetLastBuildStatus(string buildTypeId);
        Task<Settings> GetSettings();
        Task UpdateSettings(Settings settings);
    }
}