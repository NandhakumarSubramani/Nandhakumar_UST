using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthApp.Shared.Constant;

namespace HealthApp.Models
{
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
        public Patient()
        {
            CreatedDate = DateTime.Now;
            DateOfBirth = DateTime.Now.Date;
        }
    }
}