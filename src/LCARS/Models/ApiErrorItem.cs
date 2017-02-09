namespace LCARS.Models
{
    public class ApiErrorItem
    {
        public string Id { get; set; }
        public string Links { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Source { get; set; }
        public string Meta { get; set; }
    }
}