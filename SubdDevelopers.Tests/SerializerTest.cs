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
            var validChilds = validXml.ChildNodes;
            var resultChilds = resultXml.ChildNodes;

            Assert.Equal(validChilds.Count, resultChilds.Count);
        }

        private void reportProgress(int progressValue)
        {
            // Ignored
        }
    }
}
