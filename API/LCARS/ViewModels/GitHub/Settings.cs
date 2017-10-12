using System.Collections.Generic;

namespace LCARS.ViewModels.GitHub
{
    public class Settings
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Owner { get; set; }
        public string BaseUrl { get; set; }
        public List<string> Repositories { get; set; }
        public int BranchThreshold { get; set; }
        public int PullRequestThreshold { get; set; }
    }
}