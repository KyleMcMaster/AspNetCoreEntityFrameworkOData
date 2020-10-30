using AspNetCoreEntityFrameworkOData.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

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
