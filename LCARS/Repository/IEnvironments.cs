using System.Collections.Generic;
using LCARS.Models.Environments;

namespace LCARS.Repository
{
	public interface IEnvironments
	{
		IEnumerable<Tenant> Get(string filePath);

		void Update(string filePath, string tenant, string environment, string status);
	}
}