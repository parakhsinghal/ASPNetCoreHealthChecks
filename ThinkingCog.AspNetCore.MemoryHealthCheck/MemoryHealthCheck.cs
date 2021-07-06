using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ThinkingCog.AspNetCore.MemoryHealthCheck
{
    public class MemoryHealthCheck : IHealthCheck
    {
        uint maxMemoryAllocatedInMegabytes = default;
        UInt16 thresholdMemoryPercentage = default;

        public MemoryHealthCheck(uint maxMemoryAllocatedInMegabytes, UInt16 thresholdMemoryPercentage)
        {
            this.maxMemoryAllocatedInMegabytes = maxMemoryAllocatedInMegabytes;
            this.thresholdMemoryPercentage = thresholdMemoryPercentage;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            /*
                Psedocode:
                    1. Get the value of the memory the threshold memory from the appsettings.Environment.json file.
                    2. Evaluate the available free memory against the threshold memory.
             */          

            string healthyDescription = $"Allocated memory is below the threshold of {maxMemoryAllocatedInMegabytes} megabytes.";
            string degradedDescription = $"Allocated memory is within {thresholdMemoryPercentage}% of the {maxMemoryAllocatedInMegabytes} megabytes.";
            string unhealthyDescription = $"Allocated memory has crossed the threshold of {maxMemoryAllocatedInMegabytes} megabytes.";

            decimal memoryConsumed = 0.0M;

            try
            {
                using (Process proc = Process.GetCurrentProcess())
                {
                    memoryConsumed = GC.GetTotalMemory(false) / (1024 * 1024);
                }

                if (memoryConsumed < maxMemoryAllocatedInMegabytes * thresholdMemoryPercentage / 100)
                {
                    return Task.FromResult(HealthCheckResult.Healthy(healthyDescription));
                }
                else if (memoryConsumed == maxMemoryAllocatedInMegabytes * thresholdMemoryPercentage / 100)
                {
                    return Task.FromResult(HealthCheckResult.Degraded(degradedDescription));
                }
                else
                {
                    return Task.FromResult(HealthCheckResult.Unhealthy(unhealthyDescription));
                }
            }
            catch (Exception exception)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy(exception.Message, exception, null));
            }

            
        }
    }
}