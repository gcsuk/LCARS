using System.Threading.Tasks;
using LCARS.Models.Deployments;

namespace LCARS.Services
{
    public interface IDeploymentsService
    {
        Task<Deployments> Get();
        Task<Settings> GetSettings();
        Task UpdateSettings(Settings settings);
    }
}