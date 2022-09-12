# TartakApp
Simple shop and warehouse app project



## General info
This project is a simple warehouse and shop management application.
This app allows you to manage the product in the shop and warehouse. You can send the product from warehouse to shop too.
This application includes login panel.

## Scheme
```
           Client
             |
           Server
         /        \
        /          \
       /            \
      /              \
  Shop - RabbitMQ - Warehouse
    |                   |
    |                   |
  Shop              Werehouse
Database            Database
```
 
## Technologies
Project is created with:
* Blazor Server App - project template - C#
* .NET6.0
* Entity Framework Core 6.0.8
* MS SQL Server
* RabbitMQ 3.10
* Docker 20.10.17
* Docker-compose 3.4 
	
## Setup
To run this project, install Docker and Visual Studio 2022. Open it in VS and run with Docker Compose.
Then in PowerShell or cmd:
```
$ cd ../TartakApp
$ docker-compose up
```
Next run your browser on address: https://localhost:5001/  
You can use app.  
Login: "Admin" or "Manager"  
Password: "admin" to Admin or "manager" to Manager
