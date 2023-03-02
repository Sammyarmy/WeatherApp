using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Clients;
using WeatherApp.Commands;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    [ApiController]
    public class FavouritesController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDatabase _database;
        private readonly IWeatherClient _weatherClient;

        public FavouritesController(ILogger<WeatherForecastController> logger, IDatabase database, IWeatherClient weatherClient)
        {
            _logger = logger;
            _database = database;
            _weatherClient = weatherClient;
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpPost("/weatherforecast/favourites/{location}")]
        public IActionResult AddFavourite(string location)
        {
            var favouriteId = _database.AddFavourite(location);
            var response = new
            {
                message = "Your favourite has been added.",
                id = favouriteId
            };
            return Ok(response);
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpGet("/weatherforecast/favourites/{id}")]
        public async Task<ActionResult<WeatherForecast>> GetFavouriteWeather(int id)
        {
            var location = _database.GetFavourite(id); 
            if(string.IsNullOrWhiteSpace(location))
            {
                return NotFound();
            }
            return await _weatherClient.GetLiveWeatherAsync(location);
            
            
            
        }
    }
}