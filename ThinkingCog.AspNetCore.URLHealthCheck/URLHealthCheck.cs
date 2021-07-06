using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ThinkingCog.AspNetCore.URLHealthCheck
{
    public class URLHealthCheck : IHealthCheck
    {
        string url = default;
        UInt16 healthyUpperBoundInMilliseconds, degradedLowerBoundInMilliseconds, degradedUpperBoundInMilliseconds = default;

        public URLHealthCheck(string url, UInt16 healthyUpperBoundInMilliseconds, UInt16 degradedLowerBoundInMilliseconds, UInt16 degradedUpperBoundInMilliseconds)
        {
            this.url = url;
            this.healthyUpperBoundInMilliseconds = healthyUpperBoundInMilliseconds;
            this.degradedLowerBoundInMilliseconds = degradedLowerBoundInMilliseconds;
            this.degradedUpperBoundInMilliseconds = degradedUpperBoundInMilliseconds;
        }


        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            /*
                Pseudocode:
                    1. Read the value of the URL that needs to be pinged to test the health of the API.
                    2. Read the values of the response times in milliseconds that needs to be used to determine the 
                       appropriate response.
                    3. Ping the URL and based off of the calculated resopnse times, provide an appropriate health result.
             */

            string healthyDescription = $"The URL {url} is working normally.";
            string degradedDescription = $"The URL {url} is reachable but takes more time than normal.";
            string unhealthyDescription = $"The URL {url} is not available or not functioning as expected.";

            Stopwatch timer = new Stopwatch();

            try
            {
                using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        timer.Start();
                        Task<HttpResponseMessage> response = client.SendAsync(message);
                        timer.Stop();

                        if (timer.ElapsedMilliseconds <= healthyUpperBoundInMilliseconds)
                        {
                            return Task.FromResult(HealthCheckResult.Healthy(healthyDescription));
                        }
                        else if (timer.ElapsedMilliseconds >= degradedLowerBoundInMilliseconds && timer.ElapsedMilliseconds <= degradedUpperBoundInMilliseconds)
                        {
                            return Task.FromResult(HealthCheckResult.Degraded(degradedDescription));
                        }
                        else
                        {
                            return Task.FromResult(HealthCheckResult.Unhealthy(unhealthyDescription));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy(exception.Message, exception, null));
            }
        }
    }
}
