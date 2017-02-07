using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.ViewModels.Deployments;

namespace LCARS.Services
{
    public interface IDeploymentsService
    {
        Task<IEnumerable<Deployment>> Get();
    }
}