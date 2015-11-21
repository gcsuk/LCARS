namespace LCARS.Models.Builds
{
    public class Build
    {
        public int TypeId { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public BuildProgress Progress { get; set; }
    }
}