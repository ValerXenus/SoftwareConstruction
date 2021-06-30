using SubdDevelopers.Common;

namespace SubdDevelopers.source.Common
{
    /// <summary>
    /// Фабрика для работы со списком производителей (разработчиков)
    /// </summary>
    public class DevelopersSplitFileFactory : IDeveloperFactory
    {
        public ILoadDevelopersList CreateLoadDevelopersList()
        {
            return new ListDevelopersSplitFileLoader(AppGlobalSettings.DataFileName);
        }

        public ISaveDevelopersList CreateSaveDevelopersList()
        {
            return new ListDevelopersSplitFileSaver(AppGlobalSettings.DataFileName);
        }
    }
}
