using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ThinkingCog.AspNetCore.SQLServerHealthCheck
{
    public class SQLServerHealthCheck : IHealthCheck
    {
        string connectionString, databaseName, sqlText = default;
        UInt16 healthyUpperBoundInMilliseconds, degradedLowerBoundInMilliseconds, degradedUpperBoundInMilliseconds = default;

        public SQLServerHealthCheck(
            string connectionString, 
            string databaseName, 
            string sqlText, 
            UInt16 healthyUpperBoundInMilliseconds,
            UInt16 degradedLowerBoundInMilliseconds,
            UInt16 degradedUpperBoundInMilliseconds)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;
            this.sqlText = sqlText;
            this.healthyUpperBoundInMilliseconds = healthyUpperBoundInMilliseconds;
            this.degradedLowerBoundInMilliseconds = degradedLowerBoundInMilliseconds;
            this.degradedUpperBoundInMilliseconds = degradedUpperBoundInMilliseconds;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            /*
             Pseudocode:
                1. Pick the sql query and desired response times from the configuration file.
                2. Create a command with the sql query.
                2. Execute the sql using execute scaler to only return a minimal resultset.
                   Note down the total time it took to complete the execution.
                3. Based on the response times, create the HealthCheckResult values and pass back the response.
            */           

            string healthyDescription = $"The database {databaseName} is working normally.";
            string degradedDescription = $"The database {databaseName} is in an un-optimal state.";
            string unhealthyDescription = $"The database {databaseName} is not available or not functioning as expected.";

            Stopwatch timer = new Stopwatch();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = sqlText;
                        command.CommandType = CommandType.Text;
                        command.Connection = connection;
                        timer.Start();
                        await connection.OpenAsync(cancellationToken);
                        await command.ExecuteScalarAsync();
                        timer.Stop();
                    }
                }

                if (timer.ElapsedMilliseconds <= healthyUpperBoundInMilliseconds)
                {
                    return HealthCheckResult.Healthy(healthyDescription);
                }
                else if (timer.ElapsedMilliseconds >= degradedLowerBoundInMilliseconds && timer.ElapsedMilliseconds <= degradedUpperBoundInMilliseconds)
                {
                    return HealthCheckResult.Degraded(degradedDescription);
                }
                else
                {
                    return HealthCheckResult.Unhealthy(unhealthyDescription);
                }
            }
            catch (SqlException exception)
            {
                return HealthCheckResult.Unhealthy(exception.Message, exception, null);
            }
            catch (Exception exception)
            {
                return HealthCheckResult.Unhealthy(exception.Message, exception, null);
            }
        }
    }
}
