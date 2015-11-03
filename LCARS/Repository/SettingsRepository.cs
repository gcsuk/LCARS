using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace LCARS.Repository
{
    public class SettingsRepository<T> : IRepository<T>
    {
        public T Get(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new IOException(string.Format("{0} file does not exist. Refer to ReadMe file for setup instructions.", Path.GetFileName(filePath)));
            }

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
        }

        public IEnumerable<T> GetList(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new IOException(string.Format("{0} file does not exist. Refer to ReadMe file for setup instructions.", Path.GetFileName(filePath)));
            }

            return JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(filePath));
        }

        public void Update(string filePath, T settings)
        {
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);

            File.WriteAllText(filePath, json);
        }

        public void UpdateList(string filePath, IEnumerable<T> settings)
        {
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);

            File.WriteAllText(filePath, json);
        }
    }
}