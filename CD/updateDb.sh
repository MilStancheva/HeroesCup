#!/usr/bin/env bash

set +x
export HEROESCUP_CONNECTIONSTRING="server=localhost;port=3306;database=heroes_cup_db;uid=heroescup;password=xxx"
cd /var/www/heroes-cup/
dotnet exec --runtimeconfig HeroesCup.Web.runtimeconfig.json --depsfile HeroesCup.Web.deps.json ~/.nuget/packages/microsoft.entityframeworkcore.tools/3.1.3/tools/netcoreapp2.0/any/ef.dll --verbose database update --context HeroesCupDbContext --assembly HeroesCup.Data.dll --startup-assembly HeroesCup.Web.dll --data-dir ./