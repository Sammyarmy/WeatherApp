using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using WeatherApp.Clients;
using WeatherApp.Models;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

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
            try
            {
                var result = await _weatherApi.GetLiveWeatherAsync(location);
                return result;
            }
            catch (HttpResponseException ex)
            {
                return NotFound($"Location '{location}' not found.");
            }
        }
    }
}