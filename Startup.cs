using AspNetCoreEntityFrameworkOData.Context;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;

namespace AspNetCoreEntityFrameworkOData
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>(opt => opt.UseInMemoryDatabase($"InMemory"), ServiceLifetime.Singleton);

            services.AddControllers();

            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Select().Expand().Filter().OrderBy().MaxTop(1000).Count();
                endpoints.MapODataRoute("odata", "odata", GetEdmModel());
            });

            IEdmModel GetEdmModel()
            {
                ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

                builder.EntitySet<WeatherForecast>("WeatherForecasts");

                return builder.GetEdmModel();
            }
        }
    }
}
