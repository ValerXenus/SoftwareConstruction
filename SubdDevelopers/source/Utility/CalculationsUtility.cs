namespace SubdDevelopers.source.Utility
{
    public class CalculationsUtility
    {
        /// <summary>
        /// Расчет прогресса в процентах
        /// </summary>
        /// <param name="currentProgress"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int CalculateProgress(int currentProgress, int maxValue)
        {
            return (currentProgress * 100) / maxValue;
        }
    }
}
