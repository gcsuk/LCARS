using System.Configuration;

namespace LCARS.ViewModels
{
    public abstract class BaseSettings
    {
        public bool AutoRefresh
        {
            get
            {
                var autoRefresh = false;

                if (ConfigurationManager.AppSettings["AutoRefresh"] == null ||
                    bool.TryParse(ConfigurationManager.AppSettings["AutoRefresh"], out autoRefresh))
                {
                    // No need to do anything as initialisation and/or TryParse already sorted out the boolean
                }

                return autoRefresh;
            }
        }
    }
}