using System.Collections.Generic;

namespace LCARS.ViewModels.Environments
{
	public class Tenant
	{
		public Tenant()
		{
			Environments = new List<Environment>();
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public IEnumerable<Environment> Environments { get; set; }
	}
}