using System.IO;
using LCARS.Models;
using Newtonsoft.Json;

namespace LCARS.Repository
{
    public class Common
    {
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