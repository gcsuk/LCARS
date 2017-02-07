namespace LCARS.Models.Environments
{
    public class Environment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int SiteId { get; set; }
        public virtual Site Site { get; set; }
    }
}