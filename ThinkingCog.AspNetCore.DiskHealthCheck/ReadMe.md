# Disk Health Check
The disk health check for ASP.Net Core helps keep an eye on the health of a disk desired. The disk can be the disk on which the ASP.Net Core application has been hosted or some other disk.

## Getting Started
You can use the project in three forms:
- By referencing the code as provided here on GitHub,
- By compiling the code separately and including the resulting assembly,
- By including the NuGet package - ThinkingCog.AspNetCore.DiskHealthCheck from NuGet gallery.

### Prerequisites
A valid ASP.Net Core application or API project targeting .Net Core 5.0 or above.

### Usage
Usage of the disk health check requires you to provide a couple of parameters:
 - Disk name which needs to be monitored
 - Folder name which needs to be used to create a test file
 - File name to be used for creation of a test file
 - Threshold numbers representing time in milliseconds to be used to categorize responses into different health states of healthy, degraded and unhealthy
 
 All the aforementioned values can be picked from a configuration source, like an environment specific JSON based configuration file. It is preferred that these values be decided keeping in mind the hardware configuration of the environment where the application or API is hosted. E.g. A spinning hard drive will be less performant than a solid state disk.
 
 
```json
  "DiskHealthCheckSettings": {
    "DiskName": "C:",
    "FolderName": "APIHealthCheck",
    "FileName": "DeleteMeLater",
    "HealthyUpperBoundInMilliseconds": 5,
    "DegradedLowerBoundInMilliseconds": 6,
    "DegradedUpperBoundInMilliseconds": 20
  }
```

And these values can be read in the ConfigureServices method in the Startup.cs file as 

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();            
            
    var diskHealthCheckSettingsFromConfig = Configuration.GetSection("DiskHealthCheckSettings").Get<DiskHealthCheckOptions>();

    services.AddHealthChecks()                    
            .AddDiskHealthCheck(
                diskHealthCheckSettings: diskHealthCheckSettingsFromConfig,
                name: "Disk Health Check",
                failureStatus: HealthStatus.Unhealthy,
                tags: new string[] { "disk health check", "hard disk health check", "quickcheck" });
}
```

## Versioning
We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/parakhsinghal/ASPNetCoreHealthChecks/tags).

## Authors
* **Parakh Singhal** - [ThinkingCog](http://www.thinkingcog.com)

## License
This project is licensed under the MIT License.