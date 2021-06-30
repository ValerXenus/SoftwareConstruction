using System.IO;
using System.Xml;
using SubdDevelopers.source.Common;
using Xunit;

namespace SubdDevelopers.Tests
{
    public class SerializerTest
    {
        [Fact]
        public void DevelopersListXmlSerializer_Successful()
        {
            // Чтение валиного xml из файла
            var xmlContent = File.ReadAllText("Resources\\developersTest.xml");
            var validXml = new XmlDocument();
            validXml.LoadXml(xmlContent);

            // Сериализация xml
            ILoadDevelopersList developersLoader = IoС.Container.Resolve<ILoadDevelopersList>("testLoader");
            developersLoader.SetProgressReporter(reportProgress);
            developersLoader.Execute();
            var developersList = developersLoader.DeveloperList;
            var serializedXml = SerializerXmlClient.SerializeDevelopersList(developersList);
            var resultXml = new XmlDocument();
            resultXml.LoadXml(serializedXml);

            // Проверка xml с валидным
            var validRootNode = validXml.SelectSingleNode("DevelopersList");
            var resultRootNode = resultXml.SelectSingleNode("DevelopersList");

            Assert.NotNull(validRootNode);
            Assert.NotNull(resultRootNode);

            var validChildren = validRootNode.ChildNodes;
            var resultChildren = resultRootNode.ChildNodes;

            Assert.Equal(validChildren.Count, resultChildren.Count);

            for (var i = 0; i < validChildren.Count; i++)
            {
                var validChildElements = validChildren[i].ChildNodes;
                var resultChildElements = resultChildren[i].ChildNodes;
                
                Assert.Equal(validChildElements[0].InnerText, resultChildElements[0].InnerText);
                Assert.Equal(validChildElements[1].InnerText, resultChildElements[1].InnerText);
                Assert.Equal(validChildElements[2].InnerText, resultChildElements[2].InnerText);
            }
        }

        private void reportProgress(int progressValue)
        {
            // Ignored
        }
    }
}
