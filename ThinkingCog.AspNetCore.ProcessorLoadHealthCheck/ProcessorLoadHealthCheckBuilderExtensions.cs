using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkingCog.AspNetCore.ProcessorLoadHealthCheck
{
    public static class ProcessorLoadHealthCheckBuilderExtensions
    {
        private const string defaultName = "Processor Load Health Check";

        public static IHealthChecksBuilder AddProcessorLoadhealthCheck(
            this IHealthChecksBuilder builder,
            ProcessorLoadHealthCheckOptions processorLoadHealthCheckSettings,
            string name = defaultName,
            HealthStatus failureStatus = HealthStatus.Unhealthy,
            string[] tags = default)
        {
            UInt16 maxCPUAllocationInPercentage = processorLoadHealthCheckSettings.MaxCPUAllocationInPercentage;
            UInt16 thresholdCPULoadPercentage = processorLoadHealthCheckSettings.ThresholdCPULoadPercentage;

            return builder.Add(new HealthCheckRegistration(
                    name,
                    new ProcessorLoadHealthCheck(maxCPUAllocationInPercentage, thresholdCPULoadPercentage),
                    failureStatus,
                    tags));
        }
    }
}
