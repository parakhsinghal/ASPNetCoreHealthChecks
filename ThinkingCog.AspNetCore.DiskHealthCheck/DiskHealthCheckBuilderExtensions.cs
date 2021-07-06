using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkingCog.AspNetCore.DiskHealthCheck
{
   public static class DiskHealthCheckBuilderExtensions
    {
        private const string defaultName = "SQL Server Health Check";

        public static IHealthChecksBuilder AddDiskHealthCheck(
            this IHealthChecksBuilder builder,
            DiskHealthCheckOptions diskHealthCheckSettings,
            string name = defaultName,
            HealthStatus failureStatus = HealthStatus.Unhealthy,
            IEnumerable<string> tags = default)
        {
            string diskName = diskHealthCheckSettings.DiskName;
            string folderName = diskHealthCheckSettings.FolderName;
            string fileName = diskHealthCheckSettings.FileName;
            UInt16 healthyUpperBoundInMilliseconds = diskHealthCheckSettings.HealthyUpperBoundInMilliseconds;
            UInt16 degradedLowerBoundInMilliseconds = diskHealthCheckSettings.DegradedLowerBoundInMilliseconds;
            UInt16 degradedUpperBoundInMilliseconds = diskHealthCheckSettings.DegradedUpperBoundInMilliseconds;

            return builder.Add(new HealthCheckRegistration(
                    name,
                    new DiskHealthCheck(diskName, folderName, fileName, healthyUpperBoundInMilliseconds, degradedLowerBoundInMilliseconds, degradedUpperBoundInMilliseconds),
                    failureStatus, 
                    tags));
        }
    }
}
