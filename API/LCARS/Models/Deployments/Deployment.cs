namespace LCARS.Models.Deployments
{
    public class Deployment
    {
        public string ProjectGroupId { get; set; }

        public string ProjectGroup { get; set; }

        public string ProjectId { get; set; }

        public string Project { get; set; }

        public string EnvironmentId { get; set; }

        public string Environment { get; set; }

        public string ReleaseVersion { get; set; }

        public string State { get; set; }

        public string CompletedTime { get; set; }

        public string Duration { get; set; }

        public bool HasWarningsOrErrors { get; set; }

        public override string ToString()
        {
            return Project + " " + Environment + " " + ReleaseVersion + " " + State + " " + Duration + " " + CompletedTime;
        }
    }
}