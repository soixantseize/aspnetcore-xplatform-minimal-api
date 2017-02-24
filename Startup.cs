using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;


namespace RoutingSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfigurationRoot Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // Routes must configured in Configure
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole();

            var serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();

            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapGet("hello/{name}", context =>
            {
                var name = context.GetRouteValue("name");
                // This is the route handler when HTTP GET "hello/<anything>"  matches
                // To match HTTP GET "hello/<anything>/<anything>, 
                // use routeBuilder.MapGet("hello/{*name}"
                return context.Response.WriteAsync($"Hi, {name}!");
            });            

            var routes = routeBuilder.Build();
            app.UseRouter(routes);
        }
    }
}
