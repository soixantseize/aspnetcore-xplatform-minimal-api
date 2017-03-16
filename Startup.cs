using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AspNetCoreBlogService.Controllers;
using AspNetCoreBlogService.Data.Extensions;
using AspNetCoreBlogService.Data;

namespace AspNetCoreBlogService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddDbContext<ApplicationDbContext>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
        
            //Create DB on startup
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                 var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                 context.Database.Migrate();
                 context.EnsureSeedData();
            }

            var bc = new BlogController().Routes(app);
            app.UseRouter(bc);
        }
    }
}
