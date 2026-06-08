using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthApp.Models
{
    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    public class Appointment
    {
        public int AppointmentId { get; set; }
        public Patient Patient { get; set; } = default;
        public Doctor Doctor { get; set; } = default;

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; }

        [Required(ErrorMessage = "Time slot is required")]
        public string TimeSlot { get; set; } = string.Empty;

        public AppointmentStatus Status { get; private set; }
            = AppointmentStatus.Pending;


        public string CancellationReason { get; private set; }
        public void Confirm()
        {
            if (Status != AppointmentStatus.Cancelled)
            {
                Status = AppointmentStatus.Confirmed;
            }
        }
        public void Cancel(string reason)
        {
            if (Status != AppointmentStatus.Completed)
            {
                Status = AppointmentStatus.Cancelled;

                CancellationReason = reason;
            }
        }
        public void Complete()
        {
            if (Status != AppointmentStatus.Cancelled)
            {
                Status = AppointmentStatus.Completed;
            }
        }
    }
}