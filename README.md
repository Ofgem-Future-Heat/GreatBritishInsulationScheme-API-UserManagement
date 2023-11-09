 # User Management Rep
 TODO: Read me content for User Management
 
 # Users Database Project 
 # Introduction 
The purpose of the Ofgem.GBI.UsersDatabase.Infrastructure and Domain projects are to provide core data access and domain libraries for GBI applications and services.  The libraries are created as nuget packages and are available from the 'Ofgem Eserve' artifacts feed.

# Getting Started
**1. The repo consists of two projects and a 'DatabaseScripts' folder:**

- Ofgem.GBI.UsersDatabase.Domain project

    This project contains only Entities, Dto classes and Enums.

				-- Entities
                |
                -- Dtos
                |
                -- Enums


- Ofgem.GBI.UsersDatabase.Infrastructure projects, 

    This project contains Users DBContext classes and the generated Migration Scripts.
    
    This project is intended to interact with 'Repository' services which provides a layer of abstraction for the consuming services e.g. microservices, apis etc.
  
                -- Migrations
                |
                -- Persistence
            

- The 'DatabaseScripts' folder contains customised SQL script files.

**2. Adding a new Entity and building migration scripts**

- In the respective Ofgem.GBI.UsersDatabase.Infrastructure project:
    - Add your new entity class in the correct 'Entities' folder.  
    - Your entity class should inherit the 'EntityBase' class.
    - Add the following line to the relevant DBContext file.

            public DbSet<xxxx> xxxxs { get; set; }

    Where *xxxx* is your new entity class.  The property name of the your entity class in this file should be plural.

- Open the 'Developer Powershell' terminal in Visual Studio:
    - Make sure the folder path in the terminal is set to the Ofgem.GBI.UsersDatabase.Infrastructure.proj's folder path e.g. 'c:\repos\Ofgem.GBI.UsersDatabase.Infrastructure'
    - Run the following command to generate the migration script:
    
            dotnet ef migrations add xxxx --project yyyy --startup-project zzzz

    Where:
    - *xxxx* is the name of your new entity class
    - *yyyy* is the database Infrastructure project
    - *zzzz* is the API project


- View the 'Migrations' folder to check that the migration files have been generated.

*If you need to remove the *last* generated migration files then run the following command.*
        
            dotnet ef migrations remove ---project yyyy --startup-project zzzz
        
        Where:
        - *yyyy* is the database Infrastructure project
        - *zzzz* is the API project

**3. Updating databases**
- Run the following command to run migration scripts against the relevant database.

            dotnet ef database update --project yyyy --startup-project zzzz
    Where:
    - *yyyy* is the database Infrastructure project
    - *zzzz* is the API project
    
- Verify that the database and tables have been created by viewing the database.
