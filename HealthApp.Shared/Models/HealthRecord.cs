using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.Shared.Models
{
    public class HealthRecord
    {
        public int RecordId { get; set; }
        public Patient Patient { get; set; } = default;
        public Doctor Doctor { get; set; } = default;
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; } = string.Empty;


    }
}