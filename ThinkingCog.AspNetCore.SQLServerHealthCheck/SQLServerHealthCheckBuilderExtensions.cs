using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;

namespace ThinkingCog.AspNetCore.SQLServerHealthCheck
{
    public static class SQLServerHealthCheckBuilderExtensions
    {
        private const string defaultName = "SQL Server Health Check";

        public static IHealthChecksBuilder AddSQLServerHealthCheck(this IHealthChecksBuilder builder,
            SQLServerHealthCheckOptions sqlServerHealthCheckSettings,
            string name = defaultName,
            HealthStatus? failureStatus = HealthStatus.Unhealthy,
            IEnumerable<string> tags = default)
        {
            string connectionString = sqlServerHealthCheckSettings.ConnectionString;
            string databaseName = sqlServerHealthCheckSettings.DatabaseName;
            string sqlText = sqlServerHealthCheckSettings.SQLText;
            UInt16 healthyUpperBoundInMilliseconds = sqlServerHealthCheckSettings.HealthyUpperBoundInMilliseconds;
            UInt16 degradedLowerBoundInMilliseconds = sqlServerHealthCheckSettings.DegradedLowerBoundInMilliseconds;
            UInt16 degradedUpperBoundInMilliseconds = sqlServerHealthCheckSettings.DegradedUpperBoundInMilliseconds;

            return builder.Add(new HealthCheckRegistration(
                    name,
                    new SQLServerHealthCheck(connectionString, databaseName, sqlText, healthyUpperBoundInMilliseconds, degradedLowerBoundInMilliseconds, degradedUpperBoundInMilliseconds),
                    failureStatus,
                    tags));
        }
    }
}
