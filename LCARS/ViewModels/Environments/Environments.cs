using System.Collections.Generic;

namespace LCARS.ViewModels.Environments
{
    public class Environments : BaseSettings
    {
        public IEnumerable<Tenant> Tenants { get; set; }
    }
}