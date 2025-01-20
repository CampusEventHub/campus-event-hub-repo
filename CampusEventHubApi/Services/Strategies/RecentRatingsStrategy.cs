using System.Collections.Generic;
using System.Linq;

namespace CampusEventHubApi.Services.Strategies
{
    public class RecentRatingsStrategy : IRatingCalculationStrategy
    {
        public double CalculateAverageRating(IEnumerable<int> ratings)
        {
            if (!ratings.Any()) return 0;

            // Zadnje tri ocjene imaju veću težinu
            var recentRatings = ratings.Reverse().Take(3);
            return recentRatings.Average();
        }
    }
}
