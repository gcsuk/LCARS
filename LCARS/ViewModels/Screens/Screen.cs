using System.Collections.Generic;

namespace LCARS.ViewModels.Screens
{
    public class Screen
    {
        public Screen()
        {
            Boards = new List<Board>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Board> Boards { get; set; }
    }
}