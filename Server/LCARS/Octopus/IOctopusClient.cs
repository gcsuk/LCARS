using LCARS.Octopus.Models;
using Refit;

namespace LCARS.Octopus;

public interface IOctopusClient
{
    [Get("/dashboard")]
    Task<Dashboard> GetDashboard([Header("X-Octopus-ApiKey")] string apiKey);
}