using System.Collections.Generic;
using System.Linq;

namespace CampusEventHubApi.Services.Strategies
{
    public class SimpleAverageStrategy : IRatingCalculationStrategy
    {
        public double CalculateAverageRating(IEnumerable<int> ratings)
        {
            if (!ratings.Any()) return 0;
            return ratings.Average();
        }
    }
}
