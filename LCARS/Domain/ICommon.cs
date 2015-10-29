using LCARS.ViewModels;

namespace LCARS.Domain
{
    public interface ICommon
    {
        Boards SelectBoard();

        RedAlert GetRedAlert(string fileName);

        void UpdateRedAlert(string fileName, bool isEnabled, string targetDate, string alertType);
    }
}