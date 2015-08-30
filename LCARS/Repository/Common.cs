using System.Xml.Linq;
using LCARS.Models;

namespace LCARS.Repository
{
    public class Common : ICommon
    {
        public RedAlert GetRedAlert(string fileName)
        {
            XDocument doc = XDocument.Load(fileName);

            if (doc.Root == null)
                return null;

            return new RedAlert
            {
                IsEnabled = doc.Root.Element("IsEnabled").Value == "1",
                TargetDate = doc.Root.Element("TargetDate").Value,
            };
        }

        public void UpdateRedAlert(string fileName, bool isEnabled, string targetDate)
        {
            XDocument doc = XDocument.Load(fileName);

            if (doc.Root == null)
                return;

            doc.Root.Element("IsEnabled").Value = (isEnabled ? "1" : "0");
            doc.Root.Element("TargetDate").Value = targetDate;

            doc.Save(fileName);
        }
    }
}