using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.API.Models
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
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string InsuranceId { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }


        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<HealthRecord> HealthRecords { get; set; }
        public Patient()
        {
            CreatedDate = DateTime.Now;
            DateOfBirth = DateTime.Now.Date;
        }
    }
}