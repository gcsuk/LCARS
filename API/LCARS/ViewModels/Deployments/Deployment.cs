using Newtonsoft.Json;

namespace LCARS.ViewModels.Deployments
{
    public class Deployment
    {
        [JsonProperty("env")]
        public string Environment { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
    }
}