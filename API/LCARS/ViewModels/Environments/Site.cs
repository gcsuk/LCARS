using System.Collections.Generic;

namespace LCARS.ViewModels.Environments
{
	public class Site
	{
		public Site()
		{
			Environments = new List<Environment>();
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public IEnumerable<Environment> Environments { get; set; }
	}
}