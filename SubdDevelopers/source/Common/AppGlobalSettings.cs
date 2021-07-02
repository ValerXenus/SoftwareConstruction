using SubdDevelopers.Utility;

namespace SubdDevelopers.Common
{
    public static class AppGlobalSettings
    {
        private static string _logPath;

        private static string _dataFileName;

        private static string _newDataFileName;

        public static string LogPath => _logPath;

        public static string DataFileName => _dataFileName;

        public static string NewDataFileName => _newDataFileName;

        public static void Initialize()
        {
            var appConfigUtility = new AppConfigUtility();
            _logPath = appConfigUtility.AppSettings("logPath");
            _dataFileName = appConfigUtility.AppSettings("dataFileName");
            _newDataFileName = appConfigUtility.AppSettings("newDataFileName");
        }
    }
}
