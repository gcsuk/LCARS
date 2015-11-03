using LCARS.ViewModels;

namespace LCARS.Domain
{
    public interface IRedAlert
    {
        ViewModels.RedAlert GetRedAlert(string filePath);

        void UpdateRedAlert(string filePath, ViewModels.RedAlert settings);
    }
}