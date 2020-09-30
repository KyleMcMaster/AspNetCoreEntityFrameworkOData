using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace AspNetCoreEntityFrameworkOData.Tests.Controllers
{
    public class WeatherForecastsControllerTests
    {
        [Fact]
        public async void ClientShouldBeAbleToCallEndpoint()
        {
            var factory = new WebApplicationFactory<Startup>();

            var client = factory.CreateClient();

            var response = await client.GetAsync("/odata/weatherforecasts");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void ResponseShouldHaveSameCountAsSeed()
        {
            var factory = new WebApplicationFactory<Startup>();

            var client = factory.CreateClient();

            var response = await client.GetAsync("/odata/weatherforecasts");

            string content = await response.Content.ReadAsStringAsync();

            var odataResponse = JsonConvert.DeserializeObject<ODataResponse<WeatherForecast>>(content);

            odataResponse.Value.Should().HaveCount(5);
        }

        [Fact]
        public async void SelectTopOneShouldHaveCountOfOne()
        {
            var factory = new WebApplicationFactory<Startup>();

            var client = factory.CreateClient();

            var response = await client.GetAsync("/odata/weatherforecasts?$top=1");

            string content = await response.Content.ReadAsStringAsync();

            var odataResponse = JsonConvert.DeserializeObject<ODataResponse<WeatherForecast>>(content);

            odataResponse.Value.Should().HaveCount(1);
        }


        [Fact]
        public async void SelectIdPropertyShouldHaveNullOtherProperties()
        {
            var factory = new WebApplicationFactory<Startup>();

            var client = factory.CreateClient();

            var response = await client.GetAsync("/odata/weatherforecasts?$top=1&$select=id");

            string content = await response.Content.ReadAsStringAsync();

            var odataResponse = JsonConvert.DeserializeObject<ODataResponse<WeatherForecast>>(content);

            var forecast = odataResponse.Value.FirstOrDefault();

            forecast.Id.Should().NotBeEmpty();
            forecast.Date.Should().Be(default);
            forecast.Summary.Should().BeNull();
            forecast.TemperatureC.Should().Be(default);
            forecast.TemperatureF.Should().Be(32); // default (0) + 32 is 32
        }

        /// <summary>
        /// Utility class for wrapping IEnumerable<T> in the shape of odata's response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class ODataResponse<T> where T : class
        {
            public IEnumerable<T> Value { get; set; }
        }
    }
}
