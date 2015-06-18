using System;

namespace LCARS.Models
{
	public class Environment
	{
		public String Name { get; set; }

		public String Status { get; set; }

		public String StatusColour
		{
			get
			{
				switch (Status)
				{
					case "OK":
						return "statusBlue";
					case "ISSUES":
						return "statusAmber";
					case "DOWN":
						return "statusRed";
					default:
						return "statusWhite";
				}
			}
		}
	}
}