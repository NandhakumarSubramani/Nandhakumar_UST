using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthApp.Models
{
    public enum GenderType
    {
        Male,
        Female,
        Other
    }
    public class Patient
    {
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public GenderType Gender { get; set; }
        [Phone(ErrorMessage = "Enter valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string Email { get; set; } = string.Empty;
        public string InsuranceId { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public Patient()
        {
            CreatedDate = DateTime.Now;
            DateOfBirth = DateTime.Now.Date;
        }
    }
}