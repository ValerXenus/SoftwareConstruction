using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kpo4162_nmv.Lib;
using SubdDevelopers.Common;
using SubdDevelopers.source.Exceptions;
using SubdDevelopers.source.Utility;

namespace SubdDevelopers.source.Common
{
    public class ListDevelopersSplitFileLoader : ILoadDevelopersList
    {
        private List<Developer> _developerList = null;

        private ReportProgress _reportProgress = null;

        private LoadStatus _loadStatus;

        private readonly string _dataFileName = string.Empty;

        public List<Developer> DeveloperList => _developerList;

        public ListDevelopersSplitFileLoader(string dataFileName)
        {
            _dataFileName = dataFileName;
        }

        public void SetProgressReporter(ReportProgress reportProgress)
        {
            _reportProgress = reportProgress;
        }

        public void Execute()
        {
            _loadStatus = LoadStatus.None;

            if (string.IsNullOrEmpty(_dataFileName))
            {
                _loadStatus = LoadStatus.FileNameIsEmpty;
                throw new Exception("Имя файла не указано");
            }

            if (!File.Exists(_dataFileName))
            {
                _loadStatus = LoadStatus.FileNotExists;
                throw new Exception("Указанный файл не существует");
            }

            _developerList = new List<Developer>();

            var rowsCount = getRowsCount(_dataFileName);
            int currentCount = 1;

            using (var reader = new StreamReader(_dataFileName))
            {
                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine();

                    try
                    {
                        var parts = row.Split('|');
                        var currentDeveloper = new Developer();

                        currentDeveloper.Name = parts[0];
                        currentDeveloper.ProductCount = int.Parse(parts[1]);
                        currentDeveloper.Proceeds = uint.Parse(parts[2]);
                        currentDeveloper.MarketPercentage = double.Parse(parts[3]);

                        _developerList.Add(currentDeveloper);
                        currentCount++;

                        var progressValue = CalculationsUtility.CalculateProgress(currentCount, rowsCount);
                        _reportProgress(progressValue);
                    }
                    catch (Exception exception)
                    {
                        LogUtility.ErrorLog(exception.Message);
                        _loadStatus = LoadStatus.GeneralError;
                    }
                }
            }

            _loadStatus = LoadStatus.Success;
        }

        private int getRowsCount(string dataFileName)
        {
            var fileText = File.ReadAllText(dataFileName);
            var rows = fileText.Split(new[] {'\n', '\r'});

            // Отбираем не пустые строки
            var filteredRows = rows.Where(row => !string.IsNullOrEmpty(row)).ToList();

#if DEBUG
            if (filteredRows.Count == 0)
                throw new EmptyFileException("Файл загрузки пуст!");
#endif

            return filteredRows.Count;
        }
    }
}
