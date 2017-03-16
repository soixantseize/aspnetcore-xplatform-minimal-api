using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using AspNetCoreBlogService.Data;
using AspNetCoreBlogService.Data.Extensions;
using AspNetCoreBlogService.Data.Entities;
using System;

namespace AspNetCoreBlogService.Controllers
{
    public class BlogController
    {
        private BlogArticleRepository _blogArticleRepository = new BlogArticleRepository();

        public IRouter Routes(IApplicationBuilder app)
        {
            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapGet("blog", async context =>
            {
                var res = await _blogArticleRepository.GetAll();
                await HttpExtensions.WriteJson(context.Response, res, StatusCodes.Status200OK);
            });

            routeBuilder.MapGet("blog/{id:int}", async context =>
            {
                var res = await _blogArticleRepository.GetById(Convert.ToInt32(context.GetRouteValue("id")));
                if (res == null)
                   await HttpExtensions.WriteJson(context.Response, "not found", StatusCodes.Status404NotFound);
                else
                   await HttpExtensions.WriteJson(context.Response, res, StatusCodes.Status200OK);
            });

            routeBuilder.MapPost("blog", async context =>
            {
                var req = HttpExtensions.ReadFromJson<BlogArticle>(context);
                if (req == null)
                    await HttpExtensions.WriteJson(context.Response, "bad request", StatusCodes.Status400BadRequest);
                else
                {
                    var res = await _blogArticleRepository.AddAsync(req);
                    if (res > 0)
                    {
                        await HttpExtensions.WriteJson(context.Response,  res, StatusCodes.Status201Created);
                    }
                    else
                    {
                        await HttpExtensions.WriteJson(context.Response, "bad request", StatusCodes.Status400BadRequest);
                    }                  
                }
            });

            routeBuilder.MapPut("blog/{id:int}", async context =>
            {
                var req = HttpExtensions.ReadFromJson<BlogArticle>(context);
                if (req == null) 
                    await HttpExtensions.WriteJson(context.Response, "bad request", StatusCodes.Status400BadRequest);
                else
                {
                    req.Id = Convert.ToInt32(context.GetRouteValue("id"));
                    var res = await _blogArticleRepository.UpdateAsync(req);
                    if (res > 0)
                    {
                        await HttpExtensions.WriteJson(context.Response,  res, StatusCodes.Status200OK);
                    }
                    else
                    {
                        await HttpExtensions.WriteJson(context.Response, "bad request", StatusCodes.Status400BadRequest);
                    }
                }
            });

            routeBuilder.MapDelete("blog/{id:int}", async context =>
            {
                
                var res = await _blogArticleRepository.DeleteAsync(Convert.ToInt32(context.GetRouteValue("id")));
                if (res > 0)
                {
                    await HttpExtensions.WriteJson(context.Response,  res, StatusCodes.Status200OK);
                }
                else
                {
                    await HttpExtensions.WriteJson(context.Response, "bad request", StatusCodes.Status400BadRequest);
                }
                
            });               

            return  routeBuilder.Build();           
        }
    }
}
