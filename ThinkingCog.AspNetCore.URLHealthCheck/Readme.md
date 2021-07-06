# URL Health Check
The URL check helps keep a tab on the health of a URL. The URL can belong to the application hosting the health check or can point to any URL of interest.

## Getting Started
You can use the project in three forms:
- By referencing the code as provided here on GitHub,
- By compiling the code separately and including the resulting assembly,
- By including the NuGet package - ThinkingCog.AspNetCore.URLHealthCheck from NuGet gallery.

### Prerequisites
A valid ASP.Net Core application or API project targeting .Net Core 5.0 or above.

### Usage
Usage of the URL health check requires you to provide a couple of parameters:
 - URL which needs to be monitored
 - Threshold figures representing time in milliseconds based on which the health state of a URL will be determined
 
 All the aforementioned values can be picked from a configuration source, like an environment specific JSON based configuration file. It is preferred that these values be decided keeping in mind the hardware configuration of the environment where the application or API is hosted. 
 E.g. A development environment may be equipped with a less potent hosting environment than a production environment, hence, making the URL in development environment less responsive.
 
 
```json
 "URLHealthCheckSettings": {
    "URL": "https://localhost:5001/api/library/books",
    "HealthyUpperBoundInMilliseconds": 30,
    "DegradedLowerBoundInMilliseconds": 31,
    "DegradedUpperBoundInMilliseconds": 100
  }
```

And these values can be read in the ConfigureServices method in the Startup.cs file as 

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();            
            
    var processorHealthCheckSettingsFromConfig = Configuration.GetSection("ProcessorHealthCheckSettings").Get<ProcessorLoadHealthCheckOptions>();		
	
	services.AddHealthChecks()
			.AddURLHealthCheck(
                        urlHealthCheckSettings: urlHealthCheckSettingsFromConfig,
                        name: "URL Health Check",
                        failureStatus: HealthStatus.Unhealthy,
                        tags: new string[] { "url", "url health check", "quickcheck" });
}
```

## Versioning
We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/parakhsinghal/ASPNetCoreHealthChecks/tags).

## Authors
* **Parakh Singhal** - [ThinkingCog](http://www.thinkingcog.com)

## License
This project is licensed under the MIT License.