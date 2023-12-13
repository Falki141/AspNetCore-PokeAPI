# ASP.NET pokemon test project

This is an ASP.NET test project. You can create, update, get pokemons with this API.
We use an MySQL Server to store all pokemons.
This project is only for testing ASP.NET

# start up with docker
docker-compose up -d

# Commands

Migrate the database with this command:

dotnet-ef database update

Run the API (dev):
dotnet watch