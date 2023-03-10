using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using WeatherApp.Clients;
using WeatherApp.Controllers;
using WeatherApp.Models;

namespace WeatherAppTests.Controllers
{
    [TestFixture]
    public class WeatherForecastControllerTests
    {
        private WeatherForecastController _controller;
        private IWeatherClient _weatherClient;

        [SetUp]
        public void Setup()
        {
            _weatherClient = new FakeWeatherClient();
            _controller = new WeatherForecastController(null, _weatherClient);
        }

        [Test]
        public async Task GetWeatherLive_ReturnsCorrectWeatherForecast()
        {
            // Arrange
            var location = "London";

            // Act
            var result = await _controller.GetWeatherLive(location);
            var weatherForecast = result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(location, weatherForecast.location.name);
        }

        [Test]
        public async Task GetWeatherLive_ReturnsNotFoundForInvalidLocation()
        {
            // Arrange
            var location = "InvalidLocation";

            // Act
            var result = await _controller.GetWeatherLive(location);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        private class FakeWeatherClient : IWeatherClient
        {
            public async Task<ActionResult<WeatherForecast>> GetLiveWeatherAsync(string _location)
            {
                if (_location == "InvalidLocation")
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                return new WeatherForecast
                {
                    location = new Location { name = _location },
                    current = new Current
                    {
                        temp_c = 10,
                        condition = new Condition { text = "Cloudy" }
                    }
                };
            }
        }
    }
}