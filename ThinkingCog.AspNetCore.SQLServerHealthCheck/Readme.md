# SQL Server Health Check
The SQL Server health check helps keep a tab on the health of an instance of SQL Server database. The database can be the one powering the application or any instance of interest.

## Getting Started
You can use the project in three forms:
- By referencing the code as provided here on GitHub,
- By compiling the code separately and including the resulting assembly,
- By including the NuGet package - ThinkingCog.AspNetCore.SQLServerHealthCheck from NuGet gallery.

### Prerequisites
A valid ASP.Net Core application or API project targeting .Net Core 5.0 or above.

### Usage
Usage of the SQL Server health check requires you to provide a couple of parameters:
 - Connection string which would be utilized to connect to the database server and the database
 - Name of the database to be monitored
 - SQL text to be executed against the database
 - Threshold numbers representing time in milliseconds which would be used to categorize the state of the health of the database as healthy, degraded or unhealthy
 
 All the aforementioned values can be picked from a configuration source, like an environment specific JSON based configuration file. It is preferred that these values be decided keeping in mind the hardware configuration of the environment where the application or API is hosted. 
 E.g. A development environment may be equipped with a less potent database environment than a production environment.
 
 
```json
"SQLServerHealthCheckSettings": {
	"ConnectionString": "Data Source=thinkingcog-wor\\helsinki;Initial Catalog=Library;Integrated Security=True",
	"DatabaseName": "LibraryDB",
	"HealthyUpperBoundInMilliseconds": 19,
	"DegradedLowerBoundInMilliseconds": 20,
	"DegradedUpperBoundInMilliseconds": 100,
	"SQLText": "Select 1;"
}
```

And these values can be read in the ConfigureServices method in the Startup.cs file as 

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();            
            
    var sqlServerHealthCheckSettingsFromConfig = Configuration.GetSection("SQLServerHealthCheckSettings").Get<SQLServerHealthCheckOptions>();		
	
	services.AddHealthChecks()
			.AddSQLServerHealthCheck(
                        sqlServerHealthCheckSettings: sqlServerHealthCheckSettingsFromConfig,
                        name: "SQL Server Health Check",
                        failureStatus: HealthStatus.Unhealthy,
                        tags: new string[] { "sql server", "database" });
}
```

## Versioning
We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/parakhsinghal/ASPNetCoreHealthChecks/tags).

## Authors
* **Parakh Singhal** - [ThinkingCog](http://www.thinkingcog.com)

## License
This project is licensed under the MIT License.