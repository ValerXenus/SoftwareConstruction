using System.Collections.Generic;

namespace SubdDevelopers.source.Common
{
    /// <summary>
    /// Интерфейс сохранения списка производителей
    /// </summary>
    public interface ISaveDevelopersList
    {
        List<Developer> DeveloperList { get; set; }

        /// <summary>
        /// Установка делегата
        /// </summary>
        /// <param name="reportProgress"></param>
        void SetProgressReporter(ReportProgress reportProgress);

        void Execute();
    }
}
