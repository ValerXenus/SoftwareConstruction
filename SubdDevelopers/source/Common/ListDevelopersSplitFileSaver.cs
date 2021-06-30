using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SubdDevelopers.source.Common
{
    public class ListDevelopersSplitFileSaver : ISaveDevelopersList
    {
        private List<Developer> _developers = null;

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

        public void Execute()
        {
            var fileText = _developers.Aggregate(string.Empty,
                (current, developer) =>
                    current + $"{developer.Name}|{developer.ProductCount}|{developer.Proceeds}|{developer.MarketPercentage}\n");

            using (var writer = new StreamWriter(_filePath))
            {
                writer.Write(fileText);
            }
        }
    }
}
