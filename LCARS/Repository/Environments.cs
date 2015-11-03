using System.Collections.Generic;
using System.IO;
using System.Linq;
using LCARS.Models.Environments;
using Newtonsoft.Json;

namespace LCARS.Repository
{
    public class Environments : IEnvironments
    {
        public IEnumerable<Tenant> Get(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new IOException("Environments file does not exist. Refer to ReadMe file for setup instructions.");
            }

            return JsonConvert.DeserializeObject<IEnumerable<Tenant>>(File.ReadAllText(filePath));
        }

        public void Update(string filePath, string tenant, string environment, string status)
        {
            var allEnvironments = JsonConvert.DeserializeObject<IEnumerable<Tenant>>(File.ReadAllText(filePath));

            var thisEnvironment = allEnvironments.Single(t => t.Name == tenant).Environments.Single(e => e.Name == environment);

            thisEnvironment.Status = status;

            var json = JsonConvert.SerializeObject(allEnvironments, Formatting.Indented);

            File.WriteAllText(filePath, json);
        }
    }
}