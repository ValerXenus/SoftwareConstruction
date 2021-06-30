using System.Collections.Generic;

namespace SubdDevelopers.source.Common
{
    /// <summary>
    /// Интерфейс загрузки списка производителей
    /// </summary>
    public interface ILoadDevelopersList
    {
        List<Developer> DeveloperList { get; }

        /// <summary>
        /// Установка делегата
        /// </summary>
        /// <param name="reportProgress"></param>
        void SetProgressReporter(ReportProgress reportProgress);

        void Execute();
    }
}