using System.Collections.Generic;

namespace SubdDevelopers.source.Common
{
    public interface ILoadDevelopersList
    {
        List<Developer> DeveloperList { get; }

        void Execute();
    }
}