using System;

namespace LCARS.ViewModels.GitHub
{
    public class Comment
    {
        public DateTime DateCreated { get; set; }

        public User User { get; set; }

        public string Body { get; set; }
    }
}