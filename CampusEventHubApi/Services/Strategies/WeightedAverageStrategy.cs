using System.Collections.Generic;
using System.Linq;

namespace CampusEventHubApi.Services.Strategies
{
    public class WeightedAverageStrategy : IRatingCalculationStrategy
    {
        public double CalculateAverageRating(IEnumerable<int> ratings)
        {
            if (!ratings.Any()) return 0;

            var weights = ratings.Select((rating, index) => index + 1);
            var weightedSum = ratings.Zip(weights, (rating, weight) => rating * weight).Sum();
            var totalWeight = weights.Sum();

            return (double)weightedSum / totalWeight;
        }
    }
}
