namespace SubdDevelopers.source
{
    public class Developer
    {
        public string Name { get; set; }

        public int ProductCount { get; set; }

        public uint Proceeds { get; set; }

        public double MarketPercentage { get; set; }

        public Developer()
        {
            Name = string.Empty;
            ProductCount = 0;
            Proceeds = 0;
            MarketPercentage = 0;
        }
    }
}
