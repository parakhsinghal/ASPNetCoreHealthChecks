using System;
using System.ComponentModel.DataAnnotations;

namespace ThinkingCog.AspNetCore.URLHealthCheck
{
    public class URLHealthCheckOptions
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The URL cannot be null. Please provide a valid URL.")]
        [DataType(dataType: DataType.Url, ErrorMessage = "The supplied string does not match with the URL pattern.")]
        public string URL { get; set; }

        [Range(minimum: 0, maximum:65000, ErrorMessage = "The upper bound value for healthy state should be between 0 and 65000 milliseconds.")]
        public UInt16 HealthyUpperBoundInMilliseconds { get; set; }

        [Range(minimum: 0, maximum: 65000, ErrorMessage = "The lower bound value for degraded state should be between 0 and 65000 milliseconds.")]
        public UInt16 DegradedLowerBoundInMilliseconds { get; set; }

        [Range(minimum: 0, maximum: 65000, ErrorMessage = "The upper bound value for degraded state should be between 0 and 65000 milliseconds.")]
        public UInt16 DegradedUpperBoundInMilliseconds { get; set; }
    }
}
