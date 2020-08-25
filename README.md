# Pet Game Project

This Pet API project will allow you to Create Users, User Pets, Feed the Pets to mantain your pet healthy, Stroke the Pets to maintain your pet in love with you.

The soluction was implemented using the DDD Patterns with Entity Framework.
1. Apps (.NET Core WEB API, .NET Core Worker Service)
2. Domain (Our Domain model with Domain actions)
3. Infrastructure (Custom implementations as loging or file access)
4. Repository (Our DataBase Access): Using Code First.
5. Tests all our unit tests.

## Pre-Requisistes
1. You need to have installed **SQL Server 2017** or superior.
2. You need to have the **.NET Core 3.1** version enabled in your Visual Studio.

### How To execute it
1. We need to change the connection string **PetGameDataBase** contain it in appsettings.json in the projects: **Pet.Game.API** and **Pet.Game.WorkerService**
2. We need to Set multiple projects as StartUp: Right click in the solution -> Properties -> Multiple startup Projects and choose:
	- **Pet.Game.API** and set the action column to Start.
	- **Pet.Game.WorkerService** and set the action column to Start.
3. We need to run the migrations using the **Package Manager Console** set as Default Project: **Infrastructure/Pet.Game.Repository** and execute the command **update-database**
4. Check that the DB was created and execute the solution.

## Additional information about the projects

### Pet.Game.Domain
Contain all the actions related to the Domain behaviours, as Add a Pet to our entity, Decrease or Increase the Statusses with out accessing to our DataBase just business logic.

### Pet.Game.Repository
Contains all the actions need it to access to our data base. In addtion is managing the First Code that will allow us to create all our entities in the Domain model and use it to create our final database using migrations.

### Pet.Game.WorkerService

It's a very nice project add it in .NET Core that allow us to create simple hosted services for example to polling data, check statusses. I know that right now all these stuff can be made it with better approach as AWS Lambda Functions or Azure Functions, but for simple demos or projects I think that is very good.


### Pet.Game.API

Manage all our REST API, and the CRUDS like GET, POST or PUT, PATCH and DELETE if they are need it.


