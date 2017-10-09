using System.Threading.Tasks;
using LCARS.Models.Deployments;
using System.Collections.Generic;

namespace LCARS.Services
{
    public interface IDeploymentsService
    {
        Task<IEnumerable<Deployment>> Get();
        Task<Settings> GetSettings();
        Task UpdateSettings(Settings settings);
    }
}