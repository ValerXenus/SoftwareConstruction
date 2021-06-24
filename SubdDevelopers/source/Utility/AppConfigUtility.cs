using System.Configuration;

namespace SubdDevelopers.Utility
{
    public class AppConfigUtility
    {
        public string AppSettings(string name)
        {
            var receivedValue = ConfigurationManager.AppSettings[name];
            if (string.IsNullOrEmpty(receivedValue))
                return string.Empty;

            return receivedValue;
        }
    }
}
