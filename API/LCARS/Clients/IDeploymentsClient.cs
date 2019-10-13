using System.Threading.Tasks;
using Refit;
using LCARS.Models.Deployments;

namespace LCARS.Clients
{
    public interface IDeploymentsClient
    {
        [Get("/api/dashboard")]
        Task<Deployments> GetDeployments([Header("X-Octopus-ApiKey")] string apiKey);
    }
}