using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SubdDevelopers.source.Common
{
    /// <summary>
    /// Класс XML-сериализации в файл
    /// </summary>
    public class SerializerXmlClient
    {
        /// <summary>
        /// Сериализация списка производителей в XML
        /// </summary>
        /// <param name="developers"></param>
        /// <returns></returns>
        public static string SerializeDevelopersList(List<Developer> developers)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");

            var builder = new StringBuilder();

            using (var writer = XmlWriter.Create(builder, settings))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("DevelopersList");

                foreach (var developer in developers)
                {
                    writer.WriteStartElement("Developer");
                    writer.WriteAttributeString("Name", developer.Name);

                    writer.WriteElementString("ProductCount", developer.ProductCount.ToString());
                    writer.WriteElementString("Proceeds", developer.Proceeds.ToString());
                    writer.WriteElementString("MarketPercentage", developer.MarketPercentage.ToString());

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteEndDocument();
            }

            return builder.ToString();
        }
    }
}
