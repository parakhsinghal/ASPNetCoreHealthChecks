using System;
using System.ComponentModel.DataAnnotations;

namespace ThinkingCog.AspNetCore.SQLServerHealthCheck
{
    public class SQLServerHealthCheckOptions
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The connection string cannot be null. Please provide a valid connection string.")]
        public string ConnectionString { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The database name cannot be empty. Please provide a valid database name")]
        public string DatabaseName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The SQL query to be executed against the provided database cannot be empty. Please provide a valid SQL query e.g. Select 1")]
        public string SQLText { get; set; }

        [Range(minimum: 0, maximum: 65000, ErrorMessage = "The upper bound value for healthy state should be between 0 and 65000 milliseconds.")]
        public UInt16 HealthyUpperBoundInMilliseconds { get; set; }

        [Range(minimum: 0, maximum: 65000, ErrorMessage = "The lower bound value for degraded state should be between 0 and 65000 milliseconds.")]
        public UInt16 DegradedLowerBoundInMilliseconds { get; set; }

        [Range(minimum: 0, maximum: 65000, ErrorMessage = "The upper bound value for degraded state should be between 0 and 65000 milliseconds.")]
        public UInt16 DegradedUpperBoundInMilliseconds { get; set; }
    }
}
