using System.Collections.Generic;
using System.IO;
using System.Linq;
using SubdDevelopers.Common;
using SubdDevelopers.source.Common;

namespace SubdDevelopers.source.mock
{
    public class ListDevelopersSaveMock : ISaveDevelopersList
    {
        private List<Developer> _developers = null;

        private ReportProgress _reportProgress = null;

        public List<Developer> DeveloperList
        {
            get => _developers;

            set
            {
                if (_developers.Equals(value))
                    return;

                _developers = value;
            }
        }

        public ListDevelopersSaveMock()
        {
            _developers = new List<Developer>();
        }

        public void SetProgressReporter(ReportProgress reportProgress)
        {
            _reportProgress = reportProgress;
        }

        public void Execute()
        {
            _developers.Add(new Developer
            {
                Name = "Oracle",
                ProductCount = 1,
                Proceeds = 2488000000,
                MarketPercentage = 31.1d
            });

            _developers.Add(new Developer
            {
                Name = "IBM",
                ProductCount = 3,
                Proceeds = 2392000000,
                MarketPercentage = 29.9d
            });

            _developers.Add(new Developer
            {
                Name = "Microsoft",
                ProductCount = 2,
                Proceeds = 1048000000,
                MarketPercentage = 13.1d
            });

            saveDevelopersToFile();
        }

        private void saveDevelopersToFile()
        {
            var fileText = _developers.Aggregate(string.Empty,
                (current, developer) =>
                    current + $"{developer.Name}|{developer.ProductCount}|{developer.Proceeds}|{developer.MarketPercentage}\n");

            using (var writer = new StreamWriter(AppGlobalSettings.DataFileName))
            {
                writer.Write(fileText);
            }

            _reportProgress(100);
        }
    }
}
