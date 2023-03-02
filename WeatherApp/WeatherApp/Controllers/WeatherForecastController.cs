using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Clients;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherClient _weatherApi;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherClient weatherApi)
        {
            _logger = logger;
            _weatherApi = weatherApi;
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpGet("/weatherforecast/{location}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherLive(string location)
        {
            var result = await _weatherApi.GetLiveWeatherAsync(location);
            if (result.Message == "Location not found")
            {
                return NotFound("Location not found");
            }
            return result;
        }
    }
}