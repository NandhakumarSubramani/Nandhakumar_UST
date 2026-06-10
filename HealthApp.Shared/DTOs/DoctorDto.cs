using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthApp.Shared.DTOs
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Specialisation { get; set; }
        [Required]
        public int YearsOfExperience { get; set; }
        [Required]
        public decimal ConsultationFee { get; set; }
        public bool IsActive { get; set; }
    }
}
