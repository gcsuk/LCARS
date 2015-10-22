using System.Collections.Generic;
using LCARS.Models;

namespace LCARS.ViewModels
{
    public class Status : RedAlert
    {
        public IEnumerable<Tenant> Tenants { get; set; }
    }
}