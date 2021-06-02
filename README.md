# Sun Market
Sun Market is inventory and sales orders management app for small to mid-level businesses.

## Tech Stack
Full stack development tools used to build the app, including:
* REST API development with **.NET.Core**
* Front-End development with **Nuxt.js** and **Vuetify.js**
* Using **SQL** with a **PostgresSQL** database
* Unit testing nux.js with **Jest**
* Unit testing .NET.Core with **xUnit**
* End-to-end testing nuxt.js with **Cypress**

## Run Projects

### Run Server App
1. Install [PostgresSQL from EnterpriseDB](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads) database server.
2. Create the database user `sunmarket` with the password `sunmarket123`.
```sql
CREATE USER sunmarket WITH PASSWORD 'sunmarket123';
```
3. Create the database `sun_market` and assign to the previous user full previliges.
```sql
CREATE DATABASE sun_market;
GRANT ALL PRIVILEGES ON DATABASE sun_market to sunmarket;
```
4. Apply the migration and update database.
```
cd ./SunMarket.Data
dotnet ef --startup-project ..\SunMarket.Web migrations add $(MigrationName)
dotnet ef --startup-project ..\SunMarket.Web database update
```

### Run Client App
1. Move to SunMarket.FrontEnd directory ```cd ./SunMarket.FrontEnd```.
2. Install dependencies ```npm install```.
3. Run the development server ```npm run dev```.

### Connect to lacal server
* Set ```VUE_APP_API_URL``` in ```.env``` to local server url.

