# Project Variables
PROJECT_NAME ?= SunMarket

.PHONY: migrations db

migrations:
			cd ./MarketSun.Data && dotnet ef --startup-project ..\SunMarket.Web migrations add $(MigrationName) && cd ..

db:
			cd ./MarketSun.Data && dotnet ef --startup-project ..\SunMarket.Web database update && cd ..