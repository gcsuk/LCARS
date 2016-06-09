using System;

namespace LCARS.ViewModels.GitHub
{
    public class ShippedPullRequest : PullRequest
    {
        public DateTime ShippedOn { get; set; }

        public string ShippedBy { get; set; }
    }
}