using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.API.Models
{
    public class HealthRecord
    {
        public int RecordId { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Prescription { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;


    }
}