using LCARS.ViewModels;

namespace LCARS.Domain
{
    public interface ICommon
    {
        Boards SelectBoard();

        RedAlert GetRedAlert(string filePath);

        void UpdateRedAlert(string filePath, RedAlert settings);
    }
}