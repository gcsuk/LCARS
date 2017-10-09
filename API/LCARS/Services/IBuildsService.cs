using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.Models.Builds;

namespace LCARS.Services
{
    public interface IBuildsService
    {
        Task<IEnumerable<Build>> GetBuilds(string buildTypeId = "");
        Task<Build> GetBuild(int buildId);
        Task<Settings> GetSettings();
        Task UpdateSettings(Settings settings);
    }
}