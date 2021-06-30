namespace SubdDevelopers.source.Common
{
    /// <summary>
    /// Интерфейс абстрактной фабрики
    /// </summary>
    public interface IDeveloperFactory
    {
        ILoadDevelopersList CreateLoadDevelopersList();

        ISaveDevelopersList CreateSaveDevelopersList();
    }
}
