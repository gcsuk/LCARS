namespace LCARS.ViewModels.Diagnostics
{
    public class DiagnosticsResult
    {
        public DiagnosticsItem BuildsRunning { get; set; }
        public DiagnosticsItem BuildProgress { get; set; }
        public DiagnosticsItem LastBuildStatus { get; set; }

        public DiagnosticsItem Deployments { get; set; }
        public DiagnosticsItem Environments { get; set; }
        public DiagnosticsItem Issues { get; set; }

        public DiagnosticsItem GitHubPullRequests { get; set; }
        public DiagnosticsItem GitHubBranches { get; set; }
        public DiagnosticsItem GitHubComments { get; set; }

        public class DiagnosticsItem
        {
            public DiagnosticsStatus Status { get; set; }
            public string StatusText => Status.ToString();
            public string ErrorMessage { get; set; }
        }
    }
}