using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.API.Models
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
        public string FullName { get; set; } = string.Empty;
        public SpecialisationType Specialisation { get; set; }
        public string DoctorPhoneNo { get; set; } = string.Empty;
        public string DoctorEmail { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
        public decimal ConsultationFee { get; set; }
        public bool IsActive { get; set; }


        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<HealthRecord> HealthRecords { get; set; }
    }
}