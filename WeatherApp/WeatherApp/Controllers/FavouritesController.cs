using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Clients;
using WeatherApp.Commands;
using WeatherApp.Models;
using WeatherApp.Models.Responses;

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
        public async Task<IActionResult> AddFavourite(string location)
        {
            var favouriteId = await _database.AddFavourite(location);
            var response = new AddFavouriteResponse
            {
                Message = "Your favourite has been added.",
                Id = favouriteId
            };
            return Ok(response);
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpGet("/weatherforecast/favourites/{id}")]
        public async Task<ActionResult<WeatherForecast>> GetFavouriteWeather(int id)
        {
            var location = await _database.GetFavourite(id); 
            if(string.IsNullOrWhiteSpace(location))
            {
                return NotFound();
            }
            return await _weatherClient.GetLiveWeatherAsync(location);
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpDelete("/weatherforecast/favourites/{id}")]
        public async Task<IActionResult> DeleteFavouriteWeather(int id)
        {
            var isDeleted = await _database.DeleteFavourite(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            var response = new DeleteFavouriteResponse { IsDeleted = isDeleted, Message = $"{id} has been deleted"};
            return Ok(response);
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpPut("/weatherforecast/favourites/{id}/{location}")]
        public async Task<ActionResult<WeatherForecast>> UpdateFavouriteWeather(int id, string location)
        {
            throw new NotImplementedException();
        }
    }
}