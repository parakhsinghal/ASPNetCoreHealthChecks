using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ThinkingCog.AspNetCore.ProcessorLoadHealthCheck
{
    public class ProcessorLoadHealthCheckOptions
    {
        [Required(ErrorMessage ="Maximum CPU allocation is required. Please provide a valid number.")]
        [Range(1,100, ErrorMessage ="The range for the maximum CPU allocation in percentage terms is between 1 and 100.")]
        public UInt16 MaxCPUAllocationInPercentage { get; set; }

        [Required(ErrorMessage ="The threshold CPU percentage is required. Please provide a valid number.")]
        [Range(1,100, ErrorMessage ="The range for the threshold CPU load in percentage terms is between 1 and 100.")]
        public UInt16 ThresholdCPULoadPercentage { get; set; }
    }
}
