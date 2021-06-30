using SubdDevelopers.source.mock;

namespace SubdDevelopers.source.Common
{
    /// <summary>
    /// Фабрика моков для тестового списка производителей (разработчиков)
    /// </summary>
    public class DevelopersTestFactory : IDeveloperFactory
    {
        public ILoadDevelopersList CreateLoadDevelopersList()
        {
            return new ListDevelopersLoadMock();
        }

        public ISaveDevelopersList CreateSaveDevelopersList()
        {
            return new ListDevelopersSaveMock();
        }
    }
}
