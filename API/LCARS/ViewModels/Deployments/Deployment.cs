using System.Text.Json.Serialization;

namespace LCARS.ViewModels.Deployments
{
    public class Deployment
    {
        [JsonPropertyName("env")]
        public string Environment { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
    }
}