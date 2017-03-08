using System.Collections.Generic;
using LCARS.ViewModels.Environments;

namespace LCARS.Services
{
    public interface IEnvironmentsService
    {
        IEnumerable<Site> GetStatus();
    }
}