using System;
using System.Collections.Generic;

namespace LCARS.Models
{
	public class Dependency
	{
		public Dependency()
		{
			Environments = new List<Environment>();
		}

		public String Name { get; set; }

		public List<Environment> Environments { get; set; }
	}
}