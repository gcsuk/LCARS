using System.Collections.Generic;

namespace LCARS.Models.Environments
{
	public class Site
	{
		public Site()
		{
			Environments = new List<SiteEnvironment>();
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public List<SiteEnvironment> Environments { get; set; }
	}
}