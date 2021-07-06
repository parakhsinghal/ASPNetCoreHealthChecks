using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ThinkingCog.AspNetCore.DiskHealthCheck
{
    public class DiskHealthCheckOptions
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Disk name cannot be empty. Please provide a valid disk name.")]
        public string DiskName { get; set; }
        
        [Required(AllowEmptyStrings =false, ErrorMessage = "Folder name cannot be empty. Please provide a valid folder name.")]
        public string FolderName { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="File name cannot be an empty string. Please provide a valid file name.")]
        public string FileName { get; set; }

        [Range(minimum: 0, maximum: 65000, ErrorMessage = "The upper bound value for healthy state should be between 0 and 65000 milliseconds.")]
        public UInt16 HealthyUpperBoundInMilliseconds { get; set; }

        [Range(minimum: 0, maximum: 65000, ErrorMessage = "The lower bound value for degraded state should be between 0 and 65000 milliseconds.")]
        public UInt16 DegradedLowerBoundInMilliseconds { get; set; }

        [Range(minimum: 0, maximum: 65000, ErrorMessage = "The upper bound value for degraded state should be between 0 and 65000 milliseconds.")]
        public UInt16 DegradedUpperBoundInMilliseconds { get; set; }

    }
}
