namespace LCARS.Models
{
    public class Settings
    {
        public Credentials BuildServerCredentials { get; set; }

        public string DeploymentServerPath { get; set; }

        public string DeploymentServerKey { get; set; }

        public string IssuesUrl { get; set; }

        public string IssuesUsername { get; set; }

        public string IssuesPassword { get; set; }
    }

    public class Credentials
    {
        public string Domain { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}