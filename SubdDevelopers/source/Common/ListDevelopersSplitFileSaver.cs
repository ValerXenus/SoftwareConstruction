using System.Collections.Generic;
using System.IO;
using System.Linq;
using SubdDevelopers.source.Utility;

namespace SubdDevelopers.source.Common
{
    public class ListDevelopersSplitFileSaver : ISaveDevelopersList
    {
        private List<Developer> _developers = null;

        private ReportProgress _reportProgress = null;

        private string _filePath;

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

        public ListDevelopersSplitFileSaver(string filePath)
        {
            _developers = new List<Developer>();
            _filePath = filePath;
        }

        public void SetProgressReporter(ReportProgress reportProgress)
        {
            _reportProgress = reportProgress;
        }

        public void Execute()
        {
            var fileText = string.Empty;
            var currentProgress = 1;

            foreach (var developer in _developers)
            {
                fileText +=
                    $"{developer.Name}|{developer.ProductCount}|{developer.Proceeds}|{developer.MarketPercentage}\n";
                currentProgress++;
                var progressValue = CalculationsUtility.CalculateProgress(currentProgress, _developers.Count);
                _reportProgress(progressValue);
            }

            using (var writer = new StreamWriter(_filePath))
            {
                writer.Write(fileText);
            }
        }
    }
}
