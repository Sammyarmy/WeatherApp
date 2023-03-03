using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherApp.Clients;
using WeatherApp.Commands;
using WeatherApp.Controllers;
using WeatherApp.Models;
using WeatherApp.Models.Responses;

namespace WeatherAppTests
{
    public class FavouritesControllerTests
    {
        private FavouritesController _controller;
        private Mock<ILogger<WeatherForecastController>> _loggerMock;
        private Mock<IDatabase> _databaseMock;
        private Mock<IWeatherClient> _weatherClientMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<WeatherForecastController>>();
            _databaseMock = new Mock<IDatabase>();
            _weatherClientMock = new Mock<IWeatherClient>();
            _controller = new FavouritesController(_loggerMock.Object, _databaseMock.Object, _weatherClientMock.Object);
        }

        [Test]
        public void AddFavourite_Should_Return_Success_With_FavouriteId()
        {
            // Arrange
            var location = "New York";
            var favouriteId = 1;
            _databaseMock.Setup(x => x.AddFavourite(location)).ReturnsAsync(favouriteId);

            // Act
            var result = _controller.AddFavourite(location);


            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as AddFavouriteResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual("Your favourite has been added.", response.Message);
            Assert.AreEqual(favouriteId, response.Id);
        }

        [Test]
        public async Task GetFavouriteWeather_Should_Return_NotFound_When_Location_Is_Not_Found()
        {
            // Arrange
            var id = 1;
            _databaseMock.Setup(x => x.GetFavourite(id)).ReturnsAsync("");

            // Act
            var result = await _controller.GetFavouriteWeather(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetFavouriteWeather_Should_Return_WeatherForecast_When_Location_Is_Found()
        {
            // Arrange
            var id = 1;
            var location = "New York";
            _databaseMock.Setup(x => x.GetFavourite(id)).ReturnsAsync(location);
            var weatherForecast = new WeatherForecast();
            _weatherClientMock.Setup(x => x.GetLiveWeatherAsync(location)).ReturnsAsync(weatherForecast);

            // Act
            var result = await _controller.GetFavouriteWeather(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, weatherForecast);
        }

        [Test]
        public void DeleteFavouriteWeather_Should_Return_Success_With_IsDeleted_True()
        {
            // Arrange
            var id = 1;
            _databaseMock.Setup(x => x.DeleteFavourite(id)).ReturnsAsync(true);

            // Act
            var result = _controller.DeleteFavouriteWeather(id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as DeleteFavouriteResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(true, response.IsDeleted);
        }

        [Test]
        public void DeleteFavouriteWeather_Should_Return_NotFound_When_Favourite_Not_Found()
        {
            // Arrange
            var id = 1;
            _databaseMock.Setup(x => x.DeleteFavourite(id)).ReturnsAsync(false);

            // Act
            var result = _controller.DeleteFavouriteWeather(id);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
            var notFoundResult = result.Result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}