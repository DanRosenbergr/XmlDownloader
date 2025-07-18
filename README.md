# XmlDownloader

This Azure Function downloads XML data from a weather station every hour, converts it to JSON, and saves it to a SQL database. If the weather station is offline or unreachable, a blank record is saved with a text indicating the outage. If the XML is available but cannot be converted to JSON, an error is logged and a partial record is saved.

Requirements
- .NET 8 SDK
- Azure Functions Core Tools
- SQL Server LocalDB or Azure SQL Database

Local Development Setup


- Clone the repository:


   git clone https://github.com/DanRosenbergr/XmlDownloader.git
   cd XmlDownloader


- Create a local.settings.json file:
  
{

  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "XmlApi__Url": "https://api.weatherapi.com/v1/current.xml?key=YOUR_API_KEY&q=Ostrava&aqi=no"
  }
}

- Create appsettings.Development.json with the local database connection string:
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=XmlDownloader;Integrated Security=True;"
  }
}

- Run EF Core migration to set up the database:

- Run the function app locally:
