using CampusEventHubApi.Enums;
using CampusEventHubApi.Services;
using CampusEventHubApi.Services.Strategies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CampusEventHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly RatingService _ratingService;

        public RatingsController()
        {
            _ratingService = new RatingService();
            _ratingService.SetStrategy(new SimpleAverageStrategy());
        }

        // POST: api/Ratings/strategy
        [HttpPost("strategy")]
        public IActionResult SetStrategy([FromQuery] StrategyType strategyType)
        {
            switch (strategyType)
            {
                case StrategyType.Simple:
                    _ratingService.SetStrategy(new SimpleAverageStrategy());
                    break;
                case StrategyType.Weighted:
                    _ratingService.SetStrategy(new WeightedAverageStrategy());
                    break;
                case StrategyType.Recent:
                    _ratingService.SetStrategy(new RecentRatingsStrategy());
                    break;
                default:
                    return BadRequest("Nepoznata strategija.");
            }

            return Ok($"Strategija postavljena na {strategyType}.");
        }



        // GET: api/Ratings/average
        [HttpGet("average")]
        public IActionResult GetAverageRating([FromQuery] List<int> ratings)
        {
            try
            {
                var average = _ratingService.GetAverageRating(ratings);
                return Ok(average);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
