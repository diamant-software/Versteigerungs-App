# Diamant-Versteigerung-Plattform

This is a web app to make internal auctions in Diamant better.
It consists of web frontend and a Web API backend.

It was created during Diamant Hack Days. Therefore the code quality is not the best.

## Local development

- Run frontend in frontend folder with `npm run dev`.
- Run backend in backend folder with `dotnet run`.
- Run local Mongodb with `docker-compose up`.

## Deployment

The backend is deployed in Azure as a Web App with an Azure Cosmos DB. The frontend is deployed as a static web app in Azure.