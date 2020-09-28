using AspNetCoreEntityFrameworkOData.Context;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreEntityFrameworkOData.Controllers
{
    public class WeatherForecastsController : ODataController
    {
        private readonly MyDbContext myDbContext;

        public WeatherForecastsController(MyDbContext myDbContext) => this.myDbContext = myDbContext;

        [EnableQuery()]
        public IActionResult Get()
        {
            return Ok(myDbContext.WeatherForecasts);
        }
    }
}
