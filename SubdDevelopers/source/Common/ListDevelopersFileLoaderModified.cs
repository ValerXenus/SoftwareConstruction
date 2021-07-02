using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using SubdDevelopers.source.Utility;

namespace SubdDevelopers.source.Common
{
    public class ListDevelopersFileLoaderModified : ILoadDevelopersList
    {
        private List<Developer> _developerList = null;

        private ReportProgress _reportProgress = null;

        private readonly string _dataFileName;

        public List<Developer> DeveloperList => _developerList;

        public ListDevelopersFileLoaderModified(string dataFileName)
        {
            _dataFileName = dataFileName;
            _developerList = new List<Developer>();
        }

        public void SetProgressReporter(ReportProgress reportProgress)
        {
            _reportProgress = reportProgress;
        }

        /// <summary>
        /// Реализация нового чтения из файла
        /// </summary>
        public void Execute()
        {
            if (string.IsNullOrEmpty(_dataFileName))
                throw new Exception("Имя файла не задано");

            if (!File.Exists(_dataFileName))
                throw new FileNotFoundException("Указанный файл не найден");

            var fileBytes = File.ReadAllBytes(_dataFileName);
            var currentCount = 1;

            using (var memoryStream = new MemoryStream(fileBytes))
            {
                var currentXml = new XmlDocument();
                try
                {
                    currentXml.Load(memoryStream);
                }
                catch
                {
                    throw new XmlException("Ошибка при десериализации XML-файла");
                }

                var rootNode = currentXml.SelectSingleNode("DevelopersList");
                if (rootNode == null)
                    throw new XmlException("Документ некорректный");

                var developersNodes = rootNode.ChildNodes;
                foreach (XmlNode node in developersNodes)
                {
                    var dataRow = new DataRow();
                    var currentDeveloper = dataRow.ConvertXmlNodeToDeveloper(node);
                    _developerList.Add(currentDeveloper);
                    currentCount++;

                    var progressValue = CalculationsUtility.CalculateProgress(currentCount, developersNodes.Count);
                    _reportProgress(progressValue);
                }
            }
        }
    }
}
