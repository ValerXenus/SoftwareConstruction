using System.Xml;

namespace SubdDevelopers.source.Common
{
    public class DataRow : IDataRow<Developer>
    {
        public Developer ConvertXmlNodeToDeveloper(XmlNode node)
        {
            if (node.Attributes == null)
                throw new XmlException("В узле Developer отсутствуют атрибуты");

            if (node.ChildNodes.Count != 3)
                throw new XmlException("Некорректный формат элементов в узле Developer");

            var currentDeveloper = new Developer();
            currentDeveloper.Name = node.Attributes[0].InnerText;

            var productCountNode = node.SelectSingleNode("ProductCount");
            if (productCountNode == null)
                throw new XmlException("Не найден узел ProductCount");
            currentDeveloper.ProductCount = int.Parse(productCountNode.InnerText);

            var proceeds = node.SelectSingleNode("Proceeds");
            if (proceeds == null)
                throw new XmlException("Не найден узел Proceeds");
            currentDeveloper.Proceeds = uint.Parse(proceeds.InnerText);

            var marketPercentage = node.SelectSingleNode("MarketPercentage");
            if (marketPercentage == null)
                throw new XmlException("Не найден узел MarketPercentage");
            currentDeveloper.MarketPercentage = double.Parse(marketPercentage.InnerText);

            return currentDeveloper;
        }
    }
}
