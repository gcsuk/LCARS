using System.Collections.Generic;

namespace LCARS.ViewModels.Environments
{
    public class Environments : RedAlertStatus
    {
        public IEnumerable<Tenant> Tenants { get; set; }
    }
}