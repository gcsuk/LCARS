using LCARS.ViewModels;

namespace LCARS.Repository
{
    public interface ICommon
    {
        RedAlert GetRedAlert(string fileName);

        void UpdateRedAlert(string fileName, bool isEnabled, string targetDate, string alertType);
    }
}