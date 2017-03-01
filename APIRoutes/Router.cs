using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using RoutingSample.Models;
using RoutingSample.Helpers;

namespace RoutingSample.APIRoutes
{
    public class Router
    {
        ApplicationDbContext _Db = new ApplicationDbContext();
        public IRouter Routes(IApplicationBuilder app)
        {
            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapGet("company", async context =>
            {
                var values = await _Db.GetAll();
                await HttpExtensions.WriteJson(context.Response, values);
            });

            routeBuilder.MapGet("company/{id:int}", async context =>
            {
                var value = await _Db.Get(Convert.ToInt32(context.GetRouteValue("id")));
                if (value == null)
                {
                   await context.Response.WriteAsync("not found"); 
                }
                await HttpExtensions.WriteJson(context.Response, value);
            });

            routeBuilder.MapPost("company", async context =>
            {
                var newValue = await context.ReadFromJson<StoredValue>();
                if (newValue == null) return;

                //context.Response.StatusCode = 201;
                await HttpExtensions.WriteJson(context.Response,  await _Db.Add(newValue));
            });

            routeBuilder.MapPut("company/{id:int}", async context =>
            {
                var updatedValue = await context.ReadFromJson<StoredValue>();
                if (updatedValue == null) return;

                updatedValue.Id = Convert.ToInt32(context.GetRouteValue("id"));

                await HttpExtensions.WriteJson(context.Response,  await _Db.Update(updatedValue));
            });

            routeBuilder.MapDelete("company/{id:int}", async context =>
            {
                await _Db.Delete(Convert.ToInt32(context.GetRouteValue("id")));
                await context.Response.WriteAsync("404");
            });                   

            return  routeBuilder.Build();           
        }
    }
}