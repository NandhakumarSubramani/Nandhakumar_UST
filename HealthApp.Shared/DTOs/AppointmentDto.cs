using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.Shared.DTOs
{

    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string TimeSlot { get; set; }
        public string Status { get; set; }

        public string PatientName { get; set; }
        public string DoctorName { get; set; }
    }

}