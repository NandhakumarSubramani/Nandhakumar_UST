using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthApp.Models
{
    public enum SpecialisationType
    {
        GeneralPhysician,
        Cardiologist,
        Dermatologist,
        Neurologist,
        Orthopedic,
        Pediatrician,
        Psychiatrist,
        ENT,
        Gynecologist
    }
    public class Doctor
    {
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Doctor name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specialisation is required")]
        public SpecialisationType Specialisation { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Enter a valid phone number")]
        public string DoctorPhoneNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string DoctorEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Experience is required")]
        [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years")]
        public int YearsOfExperience { get; set; }

        [Required(ErrorMessage = "Consultation fee is required")]
        public decimal ConsultationFee { get; set; }

        public bool IsActive { get; set; }
    }
}