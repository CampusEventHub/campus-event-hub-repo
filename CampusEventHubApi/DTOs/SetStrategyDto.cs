using CampusEventHubApi.Enums;
using System.ComponentModel.DataAnnotations;

public class SetStrategyDto
{
    [Required(ErrorMessage = "StrategyType je obavezan.")]
    public StrategyType StrategyType { get; set; }
}
