namespace LCARS.ViewModels
{
    public class Settings
    {
        public string BuildServerDomain { get; set; }

        public string BuildServerUsername { get; set; }

        public string BuildServerPassword { get; set; }

        public string DeploymentServerPath { get; set; }

        public string DeploymentServerKey { get; set; }

        public string IssuesUrl { get; set; }

        public string IssuesUsername { get; set; }

        public string IssuesPassword { get; set; }

        public string GitHubUsername { get; set; }

        public string GitHubPassword { get; set; }
    }
}