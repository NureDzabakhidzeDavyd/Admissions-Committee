# Admissions-Committee 
 
## Pre-requisites 
* Docker 
#### or 
* .Net Core 6 
* MSSQL 
* Node.js 
 
## Description 
Repository containing the code for the Admissions-Committee. There 2 main components: 
* Admissions-Committee Api: API service with interactive swagger. 
* Admissions-Committee Web: Web application. 
 
## Resources 
 
* [API Swagger](http://localhost:5002) 
* [MongoDB](http://localhost:27017) 
* [Mongo Express](http://localhost:8081) 
* [Web](http://localhost:4200) 
 
## Docker 
 
Ensure .env file exist with next variables: 
 
ENVIRONMENT=Development 
GOOGLE_SECRET= 
GOOGLE_API_KEY= 
JWT_SECRET= 
 
 
### Commands 
 
 Start all services: 
 
 
docker-compose up -d 
 
 
 Start back-end services 
 
 
docker-compose up -d api mongo mongo_express  
 
 
Command to rebuild 
 
docker-compose up -d --build api web
