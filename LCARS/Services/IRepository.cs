using System.Collections.Generic;
using LCARS.Models;

namespace LCARS.Services
{
	public interface IRepository
	{
		IEnumerable<Tenant> GetStatus(string fileName);

		void UpdateStatus(string fileName, string tenant, string dependency, string environment, string status);

		AutoDeploy GetAutoDeploySettings(string fileName);

		void UpdateAutoDeploySettings(string fileName, bool isEnabled, string targetDate);

	    BuildStatus GetBuildStatus(string fileName);
	}
}