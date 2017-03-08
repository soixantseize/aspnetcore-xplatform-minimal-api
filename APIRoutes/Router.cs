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
                var res = await _Db.GetAll();
                await HttpExtensions.WriteJson(context.Response, res, StatusCodes.Status200OK);
            });

            routeBuilder.MapGet("company/{id:int}", async context =>
            {
                var res = await _Db.Get(Convert.ToInt32(context.GetRouteValue("id")));
                if (res == null)
                   await HttpExtensions.WriteJson(context.Response, "not found", StatusCodes.Status404NotFound);
                else
                   await HttpExtensions.WriteJson(context.Response, res, StatusCodes.Status200OK);
            });

            routeBuilder.MapPost("company", async context =>
            {
                var req = await context.ReadFromJson<StoredValue>();
                if (req == null)
                    await HttpExtensions.WriteJson(context.Response, "bad request", StatusCodes.Status400BadRequest);
                else
                {
                    var res = await _Db.Add(req);
                    if (res == null)
                    {
                        await HttpExtensions.WriteJson(context.Response, "bad request", StatusCodes.Status400BadRequest);
                        return;
                    }
                        
                    await HttpExtensions.WriteJson(context.Response,  res, StatusCodes.Status201Created);
                }
            });

            routeBuilder.MapPut("company/{id:int}", async context =>
            {
                var req = await context.ReadFromJson<StoredValue>();
                if (req == null) 
                    await HttpExtensions.WriteJson(context.Response, "bad request", StatusCodes.Status400BadRequest);
                else
                {
                    req.Id = Convert.ToInt32(context.GetRouteValue("id"));
                    var res = await _Db.Update(req);
                    if (res == null)
                    {
                        await HttpExtensions.WriteJson(context.Response, "bad request", StatusCodes.Status400BadRequest);
                        return;
                    }
                        
                    await HttpExtensions.WriteJson(context.Response,  res, StatusCodes.Status200OK);
                }
            });

            routeBuilder.MapDelete("company/{id:int}", async context =>
            {
                
                var res = await _Db.Delete(Convert.ToInt32(context.GetRouteValue("id")));
                if (res == null)
                    await HttpExtensions.WriteJson(context.Response,  "bad request", StatusCodes.Status400BadRequest);
                else 
                    await HttpExtensions.WriteJson(context.Response,  res, StatusCodes.Status200OK);
                
            });                   

            return  routeBuilder.Build();           
        }
    }
}