using System.Xml;

namespace SubdDevelopers.source.Common
{
    /// <summary>
    /// Интерфейс строки данных
    /// </summary>
    public interface IDataRow<T>
    {
        /// <summary>
        /// Конвертация Xml-узла в формат T
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        T ConvertXmlNodeToDeveloper(XmlNode node);
    }
}
