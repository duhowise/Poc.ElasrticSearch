using Microsoft.AspNetCore.Mvc;

namespace Poc.ElasticSearch.DemoTwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
      [HttpGet]  public IActionResult Test()
        {
            var randomValue = 0;

            try
            {
                var random = new Random();
                randomValue = random.Next(0, 100);
                var even = randomValue % 2 == 0;
                if (even)
                {
                    throw new ArgumentException("Cannot use null variables");
                }
                _logger.LogInformation("retrieved number {number} successfully", randomValue);
                return Ok(randomValue);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "exception occurred for  number {OddNumber} number was even", randomValue);

                return Problem(e.Message);
            }


        }
    }
}