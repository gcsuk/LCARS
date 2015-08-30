using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using LCARS.Models;

namespace LCARS.Repository
{
    public class Environments : IEnvironments
    {
        public IEnumerable<Tenant> Get(string fileName)
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

        public void Update(string fileName, string tenant, string dependency, string environment, string status)
        {
            XDocument doc = XDocument.Load(fileName);

            if (doc.Root == null)
                return;

            var tenantElement = doc.Root.Elements("Tenant").Single(t => t.Attribute("Name").Value == tenant);

            var dependencyElement =
                tenantElement.Element("Dependencies")
                    .Elements("Dependency")
                    .Single(d => d.Attribute("Name").Value == dependency);

            var environmentElement =
                dependencyElement.Element("Environments")
                    .Elements("Environment")
                    .Single(e => e.Attribute("Name").Value == environment);

            environmentElement.Attribute("Status").Value = status;

            doc.Save(fileName);
        }
    }
}