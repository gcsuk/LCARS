using System.ComponentModel;

namespace LCARS.Models
{
    public enum Boards
    {
        [Description("Home")] Environment = 1,
        Build = 2,
        Git = 3
    }
}