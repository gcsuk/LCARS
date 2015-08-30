using System.Collections.Generic;
using LCARS.Models;

namespace LCARS.Domain
{
    public interface IEnvironments
    {
        IEnumerable<Tenant> Get(string path);

        void Update(string path, string tenant, string dependency, string environment, string status);
    }
}