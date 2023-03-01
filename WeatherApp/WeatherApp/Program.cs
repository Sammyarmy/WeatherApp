using Microsoft.Extensions.Caching.Memory;
using WeatherApp.Clients;

namespace WeatherApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();



            builder.Services.AddHttpClient<WeatherApiClient>(client =>
            {
                client.BaseAddress = new Uri("http://api.weatherapi.com/v1/");
            });
            
            builder.Services.AddSingleton<IWeatherClient>(x => new CachedWeatherApiClient(x.GetRequiredService<WeatherApiClient>(), x.GetRequiredService<IMemoryCache>()));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}