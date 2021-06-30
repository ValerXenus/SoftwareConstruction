using System.Collections.Generic;
using SubdDevelopers.source.Common;

namespace SubdDevelopers.source.mock
{
    public class ListDevelopersLoadMock : ILoadDevelopersList
    {
        private List<Developer> _developers = null;

        private ReportProgress _reportProgress = null;

        public List<Developer> DeveloperList => _developers;

        public ListDevelopersLoadMock()
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

            _reportProgress(100);
        }
    }
}
