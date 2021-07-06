using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkingCog.AspNetCore.MemoryHealthCheck
{
    public static class MemoryCheckExtensionBuilder
    {
        private const string defaultName = "Memory Check";

        public static IHealthChecksBuilder AddMemoryHealthCheck(
                this IHealthChecksBuilder builder,
                MemoryHealthCheckOptions memoryHealthCheckSettings,
                string name = defaultName,
                HealthStatus failureStatus = HealthStatus.Unhealthy,
                string[] tags = default
            )
        {
            uint maxMemoryAllocatedInMegabytes = memoryHealthCheckSettings.MaxMemoryAllocatedInMegabytes;
            UInt16 thresholdMemoryPercentage = memoryHealthCheckSettings.ThresholdMemoryPercentage;

            return builder.Add(new HealthCheckRegistration(
                    name,
                    new MemoryHealthCheck(maxMemoryAllocatedInMegabytes,thresholdMemoryPercentage),
                    failureStatus,
                    tags
                ));
        }
    }
}
