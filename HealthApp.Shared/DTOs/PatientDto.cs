using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthApp.Shared.DTOs
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string InsuranceId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}