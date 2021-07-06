using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;

namespace ThinkingCog.AspNetCore.URLHealthCheck
{
    public static class URLHealthCheckBuilderExtensions
    {
        private const string defaultName = "URL Health Check";

        public static IHealthChecksBuilder AddURLHealthCheck(this IHealthChecksBuilder builder,
            URLHealthCheckOptions urlHealthCheckSettings,
            string name = defaultName,
            HealthStatus? failureStatus = HealthStatus.Unhealthy,
            IEnumerable<string> tags = default)
        {
            string url = urlHealthCheckSettings.URL;
            UInt16 healthyUpperBoundInMilliseconds = urlHealthCheckSettings.HealthyUpperBoundInMilliseconds;
            UInt16 degradedLowerBoundInMilliseconds = urlHealthCheckSettings.DegradedLowerBoundInMilliseconds;
            UInt16 degradedUpperBoundInMilliseconds = urlHealthCheckSettings.DegradedUpperBoundInMilliseconds;

            return builder.Add(new HealthCheckRegistration(
                name,
                new URLHealthCheck(url, healthyUpperBoundInMilliseconds, degradedLowerBoundInMilliseconds, degradedUpperBoundInMilliseconds),
                failureStatus,
                tags));
        }




    }
}
