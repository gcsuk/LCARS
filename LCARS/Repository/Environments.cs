using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using LCARS.Models.Environments;

namespace LCARS.Repository
{
    public class Environments : IEnvironments
    {
        public IEnumerable<Tenant> Get(string fileName)
        {
            var doc = XDocument.Load(fileName);

            if (doc.Root == null)
                return null;

            var tenants = new List<Tenant>();

            foreach (var tenantData in doc.Root.Elements("Tenant"))
            {
                var tenant = new Tenant
                {
                    Id = Convert.ToInt32(tenantData.Attribute("ID").Value),
                    Name = tenantData.Attribute("Name").Value
                };

                foreach (var environmentData in tenantData.Element("Environments").Elements("Environment"))
                {
                    tenant.Environments.Add(new Models.Environments.Environment
                    {
                        Name = environmentData.Attribute("Name").Value,
                        Status = environmentData.Attribute("Status").Value
                    });
                }

                tenants.Add(tenant);
            }

            return tenants;
        }

        public void Update(string fileName, string tenant, string environment, string status)
        {
            var doc = XDocument.Load(fileName);

            if (doc.Root == null)
                return;

            var tenantElement = doc.Root.Elements("Tenant").Single(t => t.Attribute("Name").Value == tenant);

            var environmentElement =
                tenantElement.Element("Environments")
                    .Elements("Environment")
                    .Single(e => e.Attribute("Name").Value == environment);

            environmentElement.Attribute("Status").Value = status;

            doc.Save(fileName);
        }
    }
}