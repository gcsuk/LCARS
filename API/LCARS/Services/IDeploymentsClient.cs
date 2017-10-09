using System.Threading.Tasks;
using Refit;
using LCARS.Models.Deployments;

namespace LCARS.Services
{
    public interface IDeploymentsClient
    {
        [Get("/api/dashboard")]
        Task<Deployments> GetDeployments([Header("X-Octopus-ApiKey")] string apiKey);
    }
}