using System.Collections.Generic;
using LCARS.ViewModels.Screens;

namespace LCARS.Domain
{
    public interface IScreens
    {
        IEnumerable<Screen> GetScreens(string filePath);

        bool UpdateScreen(string filePath, Screen screen);

        void DeleteScreen(string filePath, int id);

        void AddBoard(string filePath, Board board);

        void DeleteBoard(string filePath, int screenId, string boardId);
    }
}