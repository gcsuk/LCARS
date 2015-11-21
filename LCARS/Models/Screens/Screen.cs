using System.Collections.Generic;

namespace LCARS.Models.Screens
{
    public class Screen
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Board> Boards { get; set; }
    }
}