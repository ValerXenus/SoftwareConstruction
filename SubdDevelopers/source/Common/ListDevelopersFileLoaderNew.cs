using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using SubdDevelopers.source.Utility;

namespace SubdDevelopers.source.Common
{
    /// <summary>
    /// Новый класс чтения из файла
    /// </summary>
    public class ListDevelopersFileLoaderNew : ILoadDevelopersList
    {
        private List<Developer> _developerList = null;

        private ReportProgress _reportProgress = null;

        private readonly string _dataFileName;

        public List<Developer> DeveloperList => _developerList;

        public ListDevelopersFileLoaderNew(string dataFileName)
        {
            _dataFileName = dataFileName;
            _developerList = new List<Developer>();
        }

        public void SetProgressReporter(ReportProgress reportProgress)
        {
            _reportProgress = reportProgress;
        }

        /**
         * [Псевдокод метода чтения из файла]
         * Если поле "имя файла" пустое или равно null
         *      то возвращаем исключение "Имя файла не задано"
         *
         * Если файл с данными не существует
         *      то возвращаем исключение "Файл не найден"
         *
         * Чтение массива байтов из файла
         * Применяя MemoryStream с присвоенным массивом байт делать:
         *      Попытаться
         *          Десериализация массива байтов в XML
         *      Если исключение
         *          то выдаем исключение "Ошибка при десериализации XML"
         *
         *      Создаем переменную, и записываем в нее корневой узел DevelopersList
         *      Если переменная с DevelopersList равна null
         *          то создаем исключение "Документ некорректный"
         * 
         *      Получаем дочерние узлы корневого элемента DevelopersList изаписываем их в переменную списка
         *      Цикл по каждому дочернему элементу
         *          Если текущий узел не имеет атрибутов
         *              то создаем исключение "В узле Developer отсутствуют атрибуты"
         *
         *          Если текущий узел не имеет 3 дочерних узлов
         *              то создаем исключение "Некорректный формат элементов в узле Developer"
         *
         *          создаем временную переменную производителя
         *          по порядку записываем все свойства текущего производителя во временную переменную производителя
         *          добавить текущую переменную в список производителей
         *          Инкрементировать результат прогресса
         *
         */

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
                var currentCount = 1;

                foreach (XmlNode node in developersNodes)
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

                    _developerList.Add(currentDeveloper);
                    currentCount++;

                    var progressValue = CalculationsUtility.CalculateProgress(currentCount, developersNodes.Count);
                    _reportProgress(progressValue);
                }
            }
        }
    }
}
