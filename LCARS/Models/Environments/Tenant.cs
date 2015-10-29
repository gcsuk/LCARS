using System.Collections.Generic;

namespace LCARS.Models.Environments
{
	public class Tenant
	{
		public Tenant()
		{
			Environments = new List<Environment>();
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public List<Environment> Environments { get; set; }
	}
}