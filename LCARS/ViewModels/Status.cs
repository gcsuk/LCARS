using System.Collections.Generic;
using LCARS.Models;

namespace LCARS.ViewModels
{
    public class Status
    {
        public IEnumerable<Tenant> Tenants { get; set; }

        public bool IsRedAlertEnabled { get; set; }
    }
}