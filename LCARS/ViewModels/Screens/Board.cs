namespace LCARS.ViewModels.Screens
{
    public class Board
    {
        public int ScreenId { get; set; }

        public string Id { get; set; }

        public Boards CategoryId { get; set; }

        public string Category { get { return CategoryId.GetDescription(); } }

        public string Argument { get; set; }
    }
}