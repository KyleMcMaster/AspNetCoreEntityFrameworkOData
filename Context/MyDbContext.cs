using Microsoft.EntityFrameworkCore;

namespace AspNetCoreEntityFrameworkOData.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
            // this really should be somewhere else
            Database.EnsureCreated();

            WeatherForecasts.AddRange(WeatherForecast.SeedWeatherForecasts());

            SaveChanges();
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
