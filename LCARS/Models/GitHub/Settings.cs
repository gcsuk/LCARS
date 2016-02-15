using System.Collections.Generic;

namespace LCARS.Models.GitHub
{
    public class Settings
    {
        public string Owner { get; set; }

        public string BaseUrl { get; set; }

        public List<string> Repositories { get; set; }
    }
}