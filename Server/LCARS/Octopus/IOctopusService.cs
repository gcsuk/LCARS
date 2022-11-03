using LCARS.Configuration.Models;
using LCARS.Octopus.Responses;

namespace LCARS.Octopus;

public interface IOctopusService
{
    Task<IEnumerable<ProjectDeployments>> GetDeployments(OctopusSettings settings);
}