# HeroesCup

![Build](https://github.com/MilStancheva/HeroesCup/workflows/Build/badge.svg)
![Continuous-Delivery-UAT](https://github.com/MilStancheva/HeroesCup/workflows/Continuous-Delivery-UAT/badge.svg)

HeroesCup is a lightweight DotNet Core web application. It uses Piranha CMS.
It's goal is to enable school teams to find information about various volunteer programs. They can also keep track of their place at the yearly school contest among all school teams that participate in it. 

## Prerequisites
* DotNet Core 3.1
* [Entity Framework Core tools reference - .NET CLI](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)
* MySql

## Installation
* Clone/Download repository
* Add 'HEROESCUP_CONNECTIONSTRING' to your environmental variables as a key and your connection string itself as a value
* Go to the root projects directory
* Run: 
```
dotnet restore
dotnet build
cd HeroesCup.Data
dotnet ef database update
cd ..
cd HeroesCup.Web
dotnet run
```  

## Dependecies
* DotNet Core 3.1
* Entity Framework Core 3.1
* Piranha 8.1.0
* MySql
