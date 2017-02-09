namespace LCARS.ViewModels.Issues
{
    public class Issue
    {
        public string Id { get; set; }

        public string Summary { get; set; }

        public string Priority { get; set; }

        public string PriorityIcon { get; set; }

        public string Reporter { get; set; }

        public string Assignee { get; set; }

        public string Status { get; set; }
    }
}