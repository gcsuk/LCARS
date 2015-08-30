using System.Collections.Generic;
using LCARS.Models;

namespace LCARS.Services
{
	public interface IDomain
	{
	    Boards SelectBoard();

        IEnumerable<Tenant> GetStatus(string path);

		void UpdateStatus(string path, string tenant, string dependency, string environment, string status);

		RedAlert GetRedAlertSettings(string fileName);

		void UpdateRedAlertSettings(string fileName, bool isEnabled, string targetDate);

        IEnumerable<Build> GetBuilds(string path);

        IEnumerable<Build> GetBuildStatus(IEnumerable<int> buildTypeIds);
	}
}