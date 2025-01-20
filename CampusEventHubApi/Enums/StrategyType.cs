using System.Text.Json.Serialization;

namespace CampusEventHubApi.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StrategyType
    {
        Simple,
        Weighted,
        Recent
    }
}
