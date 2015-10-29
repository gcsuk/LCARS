using System.Collections.Generic;
using LCARS.ViewModels.Environments;

namespace LCARS.Domain
{
    public interface IEnvironments
    {
        IEnumerable<Tenant> Get(string path);

        void Update(string path, string tenant, string environment, string status);
    }
}