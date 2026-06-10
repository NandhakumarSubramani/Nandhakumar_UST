using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthApp.Shared.Constant;

namespace HealthApp.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public SpecialisationType Specialisation { get; set; }

        public string DoctorPhoneNo { get; set; } = string.Empty;

        public string DoctorEmail { get; set; } = string.Empty;

        public int YearsOfExperience { get; set; }

        public decimal ConsultationFee { get; set; }
        public bool IsActive { get; set; }
    }
}