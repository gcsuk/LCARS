using System.Collections.Generic;
using LCARS.ViewModels.Deployments;

namespace LCARS.Domain
{
    public interface IDeployments
    {
        IEnumerable<Deployment> Get();

        IEnumerable<Environment> SetEnvironmentOrder(IEnumerable<Environment> environments, string preferencesFilePath);
    }
}