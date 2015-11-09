namespace LCARS.Domain
{
    public interface ISettings
    {
        ViewModels.Settings GetSettings(string filePath);

        void UpdateSettings(string filePath, ViewModels.Settings settingsVm);
    }
}