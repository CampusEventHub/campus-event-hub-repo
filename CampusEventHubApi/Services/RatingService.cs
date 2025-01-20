using CampusEventHubApi.Services.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampusEventHubApi.Services
{
    public class RatingService
    {
        private IRatingCalculationStrategy _strategy;

        public void SetStrategy(IRatingCalculationStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public double GetAverageRating(IEnumerable<int> ratings)
        {
            if (_strategy == null)
            {
                throw new InvalidOperationException("Strategija nije postavljena.");
            }

            // Filtriraj ocjene koje su u rasponu 1 do 5
            var validRatings = ratings.Where(r => r >= 1 && r <= 5).ToList();

            if (!validRatings.Any())
            {
                throw new InvalidOperationException("Nema valjanih ocjena za izračun.");
            }

            return _strategy.CalculateAverageRating(validRatings);
        }
    }
}
