using System.Collections.Generic;
using LCARS.Models;

namespace LCARS.Services
{
	public interface IDomain
	{
	    Boards SelectBoard();

        IEnumerable<Tenant> GetStatus(string path);

		void UpdateStatus(string path, string tenant, string dependency, string environment, string status);

		AutoDeploy GetAutoDeploySettings(string fileName);

		void UpdateAutoDeploySettings(string fileName, bool isEnabled, string targetDate);

	    IEnumerable<Build> GetBuildStatus(string path);
	}
}