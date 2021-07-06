using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ThinkingCog.AspNetCore.MemoryHealthCheck
{
    public class MemoryHealthCheckOptions
    {
        [Required(ErrorMessage ="Please provide a value for the maximum allocated memory")]
        [Range(0,65000, ErrorMessage = "Please provide a value between 0 and 4,294,967,295 Megabytes.")]
        public uint MaxMemoryAllocatedInMegabytes { get; set; }

        [Required(ErrorMessage ="Please provide a value for the threshold percentage.")]
        [Range(0,100, ErrorMessage ="A valid value for the threshold percentage would be between 0 and 100.")]
        public UInt16 ThresholdMemoryPercentage { get; set; }
    }
}
