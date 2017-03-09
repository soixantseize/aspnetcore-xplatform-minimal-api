# ASP.Net Core Routing Cross Platform Microservice

##WHAT

An ASP.Net Core HTTP microservice that is lightweight, no MVC, and cross-platform, no IIS.  Create Read Update Write to PostgreSQL database out of the box thanks to [Npgsql](http://www.npgsql.org/) driver and Entity Framework Core Dependency Injection.  Includes data migration script that runs on startup and seeds initial data. Get started right away!

##WHY

I love the simplicity and portability of spinning up a RESTful app built on NodeJS and Express.  Imagine it was possible in .NET and we could leverage powerful features like Entity Framework, LINQ, and Async Await?  .NET Core now makes it possible!

##HOW

**Disclaimer**

This project borrows heavily from the following repos:

1.  [StatefulValuesMicroservice](https://github.com/jixer/dockernetcore/tree/master/samples/StatefulValuesMicroservice/src/StatefulValuesMicroservice)  blog post [here](http://www.bloggedbychris.com/2016/07/12/stateful-microservice-net-core-docker-postresql/)
   
2. [RoutingSample](https://github.com/aspnet/Docs/tree/master/aspnetcore/fundamentals/routing/sample/RoutingSample)  blog post [here](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing)
   
3. [LightweightAPI](https://github.com/filipw/aspnetcore-api-samples/tree/master/01%20Lightweight%20API%20(no%20MVC)/LightweightApi)  blog post [here](http://www.strathweb.com/2017/01/building-microservices-with-asp-net-core-without-mvc/)

**Prerequisites**

1. Install .NET Core  (https://www.microsoft.com/net/core)
    * Successfully ran application on Windows (Windows7), Mac (OSX 10.11.13), and Linux (CentOS 7.2.1511)
2. PostgreSQL (https://www.postgresql.org/)
    * Successfully tested CRUD on Windows (PostgreSQL 9.4.5) and Mac (PostgreSQL 9.5.2)
3. Update lines 22 and 23 in the file aspnetcore-routing-standalone/Models/ApplicationDbContext.cs with your PostgreSQL user name and password

To run application

1. Download repository
2. Open command prompt and navigate to repository
3. Run command "dotnet restore"
   * Please create new [issue](https://github.com/hatoro/aspnetcore-routing-portable-microservice/issues/new?title=Restore_Issue&assignee=hatoro&body=My%20Platform:______<br/>%20Operating%20System:_______<br/>%20DotNet%20Core%20Version:_____) if you are having trouble downloading dependencies
4. Run command "dotnet run"

![alt text](https://github.com/hatoro/aspnetcore-routing-standalone/blob/master/postman_scr.jpg "postman_screenshot")


