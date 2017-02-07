using System.Collections.Generic;

namespace LCARS.Models.GitHub
{
    public class Settings
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string BaseUrl { get; set; }
        public ICollection<string> Repositories { get; set; }
        public string RepositoriesString
        {
            get { return string.Join(",", Repositories); }
            set { Repositories = value.Split(','); }
        }
        public int BranchThreshold { get; set; }
        public int PullRequestThreshold { get; set; }
    }
}