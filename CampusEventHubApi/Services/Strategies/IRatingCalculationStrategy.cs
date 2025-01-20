namespace CampusEventHubApi.Services.Strategies
{
    public interface IRatingCalculationStrategy
    {
        double CalculateAverageRating(IEnumerable<int> ratings);
    }
}
