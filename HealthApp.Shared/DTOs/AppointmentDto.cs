using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthApp.Shared.DTOs
{

    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public DateTime ScheduledDate { get; set; }
        [Required]
        public string TimeSlot { get; set; }
        public string Status { get; set; }

        public string PatientName { get; set; }
        public string DoctorName { get; set; }
    }

}