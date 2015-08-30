using System.Collections.Generic;
using LCARS.Models;

namespace LCARS.Repository
{
	public interface IEnvironments
	{
		IEnumerable<Tenant> Get(string fileName);

		void Update(string fileName, string tenant, string dependency, string environment, string status);
	}
}