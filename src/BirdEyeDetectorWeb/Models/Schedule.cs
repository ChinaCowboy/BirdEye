﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BirdEyeDetector.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string PublicSchedule { get; set; }

        [Display(Name = "Public Schedule Size (bytes)")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public long PublicScheduleSize { get; set; }

        [Display(Name = "Uploaded (UTC)")]
        [DisplayFormat(DataFormatString = "{0:F}")]
        public DateTime UploadDT { get; set; }
    }
}
