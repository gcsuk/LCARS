namespace LCARS.Models
{
    public class Settings
    {
        public int Id { get; set; }

        public string BuildServerUrl { get; set; }
        public string BuildServerUsername { get; set; }
        public string BuildServerPassword { get; set; }

        public string DeploymentsServerUrl { get; set; }
        public string DeploymentsServerKey { get; set; }

        public string IssuesUrl { get; set; }
        public string IssuesUsername { get; set; }
        public string IssuesPassword { get; set; }

        public string GitHubUsername { get; set; }
        public string GitHubPassword { get; set; }
    }
}