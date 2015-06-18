using System;
using System.Collections.Generic;

namespace LCARS.Models
{
	public class Tenant
	{
		public Tenant()
		{
			Dependencies = new List<Dependency>();
		}

		public Int32 Id { get; set; }

		public String Name { get; set; }

		public List<Dependency> Dependencies { get; set; }
	}
}