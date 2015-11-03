using LCARS.Models;

namespace LCARS.Repository
{
    public interface ICommon
    {
        RedAlert GetRedAlert(string filePath);

        void UpdateRedAlert(string filePath, RedAlert settings);
    }
}