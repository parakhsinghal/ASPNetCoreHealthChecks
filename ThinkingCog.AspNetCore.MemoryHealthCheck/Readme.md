# Memory Health Check
The memory health check helps keep a tab on the health of the ASP.Net Core application in terms of memory consumption on the machine hosted.

## Getting Started
You can use the project in three forms:
- By referencing the code as provided here on GitHub,
- By compiling the code separately and including the resulting assembly,
- By including the NuGet package - ThinkingCog.AspNetCore.MemoryHealthCheck from NuGet gallery.

### Prerequisites
A valid ASP.Net Core application or API project targeting .Net Core 5.0 or above.

### Usage
Usage of the memory health check requires you to provide a couple of parameters:
 - Max memory that is allowed to be allocated to the application
 - Threshold in terms of a percentage above which the health state will be termed as degraded
 
 All the aforementioned values can be picked from a configuration source, like an environment specific JSON based configuration file. It is preferred that these values be decided keeping in mind the hardware configuration of the environment where the application or API is hosted. 
 E.g. A development environment will have less of memory available than a production environment.
 
 
```json
"MemoryHealthCheckSettings": {
  "MaxMemoryAllocatedInMegabytes": 500,
  "ThresholdMemoryPercentage": 90
}
```

And these values can be read in the ConfigureServices method in the Startup.cs file as 

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();            
            
    var memoryHealthCheckSettingsFromConfig = Configuration.GetSection("MemoryHealthCheckSettings").Get<MemoryHealthCheckOptions>();		
	
	services.AddHealthChecks()
			.AddMemoryHealthCheck(
				memoryHealthCheckSettings: memoryHealthCheckSettingsFromConfig,
				name: "Memory Health Check",
				failureStatus: HealthStatus.Unhealthy,
				tags: new string[] { "memory health check", "memory", "quickcheck" });
}
```

## Versioning
We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/parakhsinghal/ASPNetCoreHealthChecks/tags).

## Authors
* **Parakh Singhal** - [ThinkingCog](http://www.thinkingcog.com)

## License
This project is licensed under the MIT License.