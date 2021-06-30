using System.Collections.Generic;

namespace SubdDevelopers.source.Common
{
    /// <summary>
    /// Интерфейс сохранения списка производителей
    /// </summary>
    public interface ISaveDevelopersList
    {
        List<Developer> DeveloperList { get; set; }

        void Execute();
    }
}
