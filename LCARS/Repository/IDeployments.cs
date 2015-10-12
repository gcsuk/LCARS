using System.Collections.Generic;
using LCARS.Models.Deployments;

namespace LCARS.Repository
{
    public interface IDeployments
    {
        Models.Deployments.Deployments Get();

        IEnumerable<Environment> GetEnvironmentPreferences(string fileName);
    }
}