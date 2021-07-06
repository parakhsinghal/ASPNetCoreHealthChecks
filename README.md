# ASPNetCoreHealthChecks
This repository contains projects which can help you determine the state of your asp.net core application or api. The following health checks are availlable:
1. Disk health check
2. Memory health check
3. Processor load health check
4. SQL Server health check
5. URL health check

All the health checks target ASP.Net Core application built using .Net Core 5 and above. The health checks are also available in NuGet package forms in NuGet gallery.

## Disk health check
```PowerShell
Install-Package ThinkingCog.AspNetCore.DiskHealthCheck
```

## Memory health check
```PowerShell
Install-Package ThinkingCog.ASPNetCore.MemoryHealthCheck
```

## Processor load health check
```PowerShell
Install-Package ThinkingCog.ASPNetCore.ProcessorLoadHEalthCheck
```

## SQL Server health check
```PowerShell
Install-Package ThinkingCog.ASPNetCore.SQLServerHealthCheck
```

## URL health check
```PowerShell
Install-Package ThinkingCog.ASPNetCore.URLHealthCheck
```