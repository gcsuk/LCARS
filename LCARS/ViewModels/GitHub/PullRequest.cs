using System;
using System.Collections.Generic;
using System.Linq;

namespace LCARS.ViewModels.GitHub
{
    public class PullRequest
    {
        public string Repository { get; set; }

        public int Number { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string AuthorName { get; set; }

        public string AuthorAvatar { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public bool IsShipped
        {
            get { return Comments != null && Comments.Any(c => c.Body.Contains(":ship")); }
        }
    }
}