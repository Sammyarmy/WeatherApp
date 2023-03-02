namespace WeatherApp.Exceptions
{
    public class WeatherApiException : Exception
    {
        public WeatherApiException(string message) : base(message)
        {
        }
    }

    public class BadRequestException : WeatherApiException
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }

    public class NotFoundException : WeatherApiException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

    public class InternalServerErrorException : WeatherApiException
    {
        public InternalServerErrorException(string message) : base(message)
        {
        }
    }
}