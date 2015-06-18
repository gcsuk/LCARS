using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using LCARS.Models;

namespace LCARS.Services
{
	public class Repository : IRepository
	{
		public IEnumerable<Tenant> GetStatus(string fileName)
		{
			XDocument doc = XDocument.Load(fileName);

			if (doc.Root == null)
				return null;

			List<Tenant> tenants = new List<Tenant>();

			foreach (var tenantData in doc.Root.Elements("Tenant"))
			{
				var tenant = new Tenant
				{
					Id = Convert.ToInt32(tenantData.Attribute("ID").Value),
					Name = tenantData.Attribute("Name").Value
				};

				foreach (var dependencyData in tenantData.Element("Dependencies").Elements("Dependency"))
				{
					var dependency = new Dependency { Name = dependencyData.Attribute("Name").Value };

					foreach (var environmentData in dependencyData.Element("Environments").Elements("Environment"))
					{
						dependency.Environments.Add(new Models.Environment
						{
							Name = environmentData.Attribute("Name").Value,
							Status = environmentData.Attribute("Status").Value
						});
					}

					tenant.Dependencies.Add(dependency);
				}

				tenants.Add(tenant);
			}

			return tenants;
		}

		public void UpdateStatus(string fileName, string tenant, string dependency, string environment, string status)
		{
			XDocument doc = XDocument.Load(fileName);

			if (doc.Root == null) 
				return;

			var tenantElement = doc.Root.Elements("Tenant").Single(t => t.Attribute("Name").Value == tenant);

			var dependencyElement = tenantElement.Element("Dependencies").Elements("Dependency").Single(d => d.Attribute("Name").Value == dependency);

			var environmentElement = dependencyElement.Element("Environments").Elements("Environment").Single(e => e.Attribute("Name").Value == environment);
				
			environmentElement.Attribute("Status").Value = status;

			doc.Save(fileName);
		}

		public AutoDeploy GetAutoDeploySettings(string fileName)
		{
			XDocument doc = XDocument.Load(fileName);

			if (doc.Root == null)
				return null;

			AutoDeploy autoDeploy = new AutoDeploy
			{
				IsEnabled = doc.Root.Element("IsEnabled").Value == "1",
				TargetDate = doc.Root.Element("TargetDate").Value,
			};

			return autoDeploy;
		}

		public void UpdateAutoDeploySettings(string fileName, bool isEnabled, string targetDate)
		{
			XDocument doc = XDocument.Load(fileName);

			if (doc.Root == null) 
				return;

			doc.Root.Element("IsEnabled").Value = (isEnabled ? "1" : "0");
			doc.Root.Element("TargetDate").Value = targetDate;

			doc.Save(fileName);
		}
	}
}