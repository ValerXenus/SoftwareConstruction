using System;
using System.Collections.Generic;
using System.IO;
using Kpo4162_nmv.Lib;
using SubdDevelopers.Common;

namespace SubdDevelopers.source.Common
{
    public class ListDevelopersSplitFileLoader : ILoadDevelopersList
    {
        private List<Developer> _developerList = null;

        private LoadStatus _loadStatus;

        private readonly string _dataFileName = string.Empty;

        public List<Developer> DeveloperList => _developerList;

        public ListDevelopersSplitFileLoader(string dataFileName)
        {
            _dataFileName = dataFileName;
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
    }
}
