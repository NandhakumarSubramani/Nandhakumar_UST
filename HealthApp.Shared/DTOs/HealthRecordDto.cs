using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.Shared.DTOs
{
    public class HealthRecordDto
    {

        public int RecordId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public DateTime VisitDate { get; set; }
        [Required]
        public string Diagnosis { get; set; }
        [Required]
        public string Prescription { get; set; }
        public string Notes { get; set; }


        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }

    }
}
