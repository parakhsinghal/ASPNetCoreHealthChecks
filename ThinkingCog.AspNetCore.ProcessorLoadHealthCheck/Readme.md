# Processor Load Health Check
The processor load health check helps keep a tab on the health of an ASP.Net Core application in terms of processor load.

## Getting Started
You can use the project in three forms:
- By referencing the code as provided here on GitHub,
- By compiling the code separately and including the resulting assembly,
- By including the NuGet package - ThinkingCog.AspNetCore.ProcessorLoadHealthCheck from NuGet gallery.

### Prerequisites
A valid ASP.Net Core application or API project targeting .Net Core 5.0 or above.

### Usage
Usage of the processor load health check requires you to provide a couple of parameters:
 - Max processor utilization allowed to the ASP.Net Core application in terms of a percentage
 - A threshold figure expressed in terms of percentage beyond which the health of the application will be termed as degraded
 
 All the aforementioned values can be picked from a configuration source, like an environment specific JSON based configuration file. It is preferred that these values be decided keeping in mind the hardware configuration of the environment where the application or API is hosted. 
 E.g. A development environment may be equipped with a less potent hosting environment than a production environment.
 
 
```json
"ProcessorHealthCheckSettings": {
    "MaxCPUAllocationInPercentage": 20,
    "ThresholdCPULoadPercentage": 90
  }
```

And these values can be read in the ConfigureServices method in the Startup.cs file as 

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();            
            
    var processorHealthCheckSettingsFromConfig = Configuration.GetSection("ProcessorHealthCheckSettings").Get<ProcessorLoadHealthCheckOptions>();		
	
	services.AddHealthChecks()
			.AddProcessorLoadhealthCheck(
                            processorLoadHealthCheckSettings: processorHealthCheckSettingsFromConfig,
                            name: "Processor Load Health Check",
                            failureStatus: HealthStatus.Unhealthy,
                            tags: new string[] { "processor health", "processor load", "quickcheck" });
}
```

## Versioning
We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/parakhsinghal/ASPNetCoreHealthChecks/tags).

## Authors
* **Parakh Singhal** - [ThinkingCog](http://www.thinkingcog.com)

## License
This project is licensed under the MIT License.