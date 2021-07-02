using System.IO;
using System.Xml;
using SubdDevelopers.Common;
using SubdDevelopers.source.Common;
using Xunit;

namespace SubdDevelopers.Tests
{
    public class NewLoaderTests
    {
        /// <summary>
        /// Тест проверки десериализации
        /// </summary>
        [Fact]
        public void DeserializationCheck_Successful()
        {
            AppGlobalSettings.Initialize();

            // Инициализация перемешанного файла
            ILoadDevelopersList developersLoader = IoС.Container.Resolve<ILoadDevelopersList>("prodLoaderNew");
            developersLoader.SetProgressReporter(reportProgress);
            developersLoader.Execute();
            var developers = developersLoader.DeveloperList;

            // Инициализация валидных данных
            var xmlContent = File.ReadAllText("Resources\\developersTest.xml");
            var validXml = new XmlDocument();
            validXml.LoadXml(xmlContent);
            var validRootNode = validXml.SelectSingleNode("DevelopersList");
            Assert.NotNull(validRootNode);
            var validChildren = validRootNode.ChildNodes;

            for (var i = 0; i < validChildren.Count; i++)
            {
                var validChildElements = validChildren[i];

                Assert.Equal(int.Parse(validChildElements.SelectSingleNode("ProductCount")?.InnerText ?? string.Empty),
                    developers[i].ProductCount);
                Assert.Equal(uint.Parse(validChildElements.SelectSingleNode("Proceeds")?.InnerText ?? string.Empty),
                    developers[i].Proceeds);
                Assert.Equal(
                    double.Parse(validChildElements.SelectSingleNode("MarketPercentage")?.InnerText ?? string.Empty),
                    developers[i].MarketPercentage);
            }
        }

        private void reportProgress(int progressValues)
        {
            // Ignored
        }
    }
}
