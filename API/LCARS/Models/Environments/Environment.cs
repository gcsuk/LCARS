namespace LCARS.Models.Environments
{
    public class Environment
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}