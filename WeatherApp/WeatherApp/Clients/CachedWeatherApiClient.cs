using Microsoft.Extensions.Caching.Memory;

namespace WeatherApp.Clients
{
    public class CachedWeatherApiClient : IWeatherClient
    {
        private readonly IWeatherClient _weatherClient;
        private readonly IMemoryCache _memoryCache;

        public CachedWeatherApiClient(IWeatherClient weatherClient, IMemoryCache memoryCache)
        {
            _weatherClient = weatherClient;
            _memoryCache = memoryCache;
        }

        public async Task<WeatherForecast> GetLiveWeatherAsync(string location)
        {
            return await _memoryCache.GetOrCreateAsync(location,
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
                    return await _weatherClient.GetLiveWeatherAsync(entry.Key.ToString());
        });
        }
    }
}
