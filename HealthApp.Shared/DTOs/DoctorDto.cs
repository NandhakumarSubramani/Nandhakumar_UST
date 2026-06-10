using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.Shared.DTOs
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Specialisation { get; set; }
        public int YearsOfExperience { get; set; }

        public decimal ConsultationFee { get; set; }
        public bool IsActive { get; set; }
    }
}
