# ASP.Net Core Routing Standalone

This project borrows heavily from the following projects:

1.  [StatefulValuesMicroservice](https://github.com/jixer/dockernetcore/tree/master/samples/StatefulValuesMicroservice/src/StatefulValuesMicroservice)  blog post [here](http://www.bloggedbychris.com/2016/07/12/stateful-microservice-net-core-docker-postresql/)
   
2. [RoutingSample](https://github.com/aspnet/Docs/tree/master/aspnetcore/fundamentals/routing/sample/RoutingSample)  blog post [here](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing)
   
3. [LightweightAPI](https://github.com/filipw/aspnetcore-api-samples/tree/master/01%20Lightweight%20API%20(no%20MVC)/LightweightApi)  blog post [here](http://www.strathweb.com/2017/01/building-microservices-with-asp-net-core-without-mvc/)

Prerequisites

1. Install .NET Core  (https://www.microsoft.com/net/core)
    * Successfully ran application on Windows (Windows7), Mac (OSX 10.11.13), and Linux (CentOS 7.2.1511)
2. PostgreSQL (https://www.postgresql.org/)
    * Successfully tested CRUD on Windows (PostgreSQL 9.4.5) and Mac (PostgreSQL 9.5.2)
3. Update lines 22 and 23 in the file aspnetcore-routing-standalone/Models/ApplicationDbContext.cs with your PostgreSQL user name and password

To run application

1. Download repository
2. Open command prompt and navigate to repository
3. Run command "dotnet restore"
4. Run command "dotnet run"

![alt text](https://github.com/hatoro/aspnetcore-routing-standalone/blob/master/postman_scr.jpg "postman_screenshot")


