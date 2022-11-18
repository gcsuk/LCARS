namespace LCARS.Octopus.Responses;

public record ProjectDeployments
{
    public string? ProjectName { get; set; }

    public List<DeploymentModel> Deployments { get; set; } = new();

    public record DeploymentModel
    {
        public string? Environment { get; set; }
        public string? ReleaseVersion { get; set; }
        public string? State { get; set; }
        public bool HasWarningsOrErrors { get; set; }
        public string? WebUrl { get; set; }
    }
}