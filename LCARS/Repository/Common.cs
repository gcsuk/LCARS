using System.IO;
using LCARS.Models;
using Newtonsoft.Json;

namespace LCARS.Repository
{
    public class Common : ICommon
    {
        public RedAlert GetRedAlert(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new IOException("RedAlert file does not exist. Refer to ReadMe file for setup instructions.");
            }

            return JsonConvert.DeserializeObject<RedAlert>(File.ReadAllText(filePath));
        }

        public void UpdateRedAlert(string filePath, RedAlert settings)
        {
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);

            File.WriteAllText(filePath, json);
        }

        public static Settings GetSettings(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new IOException("Settings file does not exist. Refer to ReadMe file for setup instructions.");
            }

            return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(filePath));
        }
    }
}