using System.Collections.Generic;

namespace LCARS.ViewModels.Screens
{
    public class Screens
    {
        public Screens()
        {
            ScreenList = new List<Screen>();
        }

        public Dictionary<int, string> Categories { get; set; }

        public List<Screen> ScreenList { get; set; }
    }
}