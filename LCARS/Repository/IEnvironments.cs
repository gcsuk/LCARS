using System.Collections.Generic;
using LCARS.Models.Environments;

namespace LCARS.Repository
{
	public interface IEnvironments
	{
		IEnumerable<Tenant> Get(string fileName);

		void Update(string fileName, string tenant, string environment, string status);
	}
}