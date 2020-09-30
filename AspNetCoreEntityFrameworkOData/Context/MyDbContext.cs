using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AspNetCoreEntityFrameworkOData.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
            // this really should be somewhere else
            Database.EnsureCreated();

            if (!WeatherForecasts.Any())
            {
                WeatherForecasts.AddRange(WeatherForecast.SeedWeatherForecasts());
            }

            SaveChanges();
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
