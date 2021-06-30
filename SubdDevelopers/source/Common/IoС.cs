using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SubdDevelopers.Common;
using SubdDevelopers.source.mock;

namespace SubdDevelopers.source.Common
{
    /// <summary>
    /// Класс реализации IoC
    /// </summary>
    public static class IoС
    {
        private static WindsorContainer _container;

        public static WindsorContainer Container => _container;

        static IoС()
        {
            _container = new WindsorContainer();

            _container.Register(Component
                .For<ILoadDevelopersList>()
                .ImplementedBy<ListDevelopersSplitFileLoader>()
                .DependsOn(Dependency.OnValue("dataFileName", AppGlobalSettings.DataFileName))
                .LifeStyle.Transient
                .Named("prodLoader"));
            _container.Register(Component
                .For<ILoadDevelopersList>()
                .ImplementedBy<ListDevelopersLoadMock>()
                .DependsOn(Dependency.OnValue("dataFileName", AppGlobalSettings.DataFileName))
                .LifeStyle.Transient
                .Named("testLoader"));

            _container.Register(Component
                .For<ISaveDevelopersList>()
                .ImplementedBy<ListDevelopersSplitFileSaver>()
                .DependsOn(Dependency.OnValue("filePath", AppGlobalSettings.DataFileName))
                .LifeStyle.Transient
                .Named("prodSaver"));
            _container.Register(Component
                .For<ISaveDevelopersList>()
                .ImplementedBy<ListDevelopersSaveMock>()
                .DependsOn(Dependency.OnValue("filePath", AppGlobalSettings.DataFileName))
                .LifeStyle.Transient
                .Named("testSaver"));
        }
    }
}
