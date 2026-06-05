using HealthApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.Database
{
    public static class AppointmentDb
    {
        public static List<Appointment> Appointments { get; set; }

        static AppointmentDb()
        {
            Appointments = new List<Appointment>
            {
                new Appointment
                {
                    AppointmentId = 1,
                    Patient = PatientDb.Patients[0],
                    Doctor = DoctorDb.Doctors[0],
                    ScheduledDate = DateTime.Now.AddDays(1),
                    TimeSlot = "10:00 AM"
                },
                new Appointment
                {
                    AppointmentId = 2,
                    Patient = PatientDb.Patients[1],
                    Doctor = DoctorDb.Doctors[1],
                    ScheduledDate = DateTime.Now.AddDays(2),
                    TimeSlot = "11:00 AM"
                },
                new Appointment
                {
                    AppointmentId = 3,
                    Patient = PatientDb.Patients[2],
                    Doctor = DoctorDb.Doctors[2],
                    ScheduledDate = DateTime.Now.AddDays(3),
                    TimeSlot = "12:00 PM"
                },
                new Appointment
                {
                    AppointmentId = 4,
                    Patient = PatientDb.Patients[3],
                    Doctor = DoctorDb.Doctors[3],
                    ScheduledDate = DateTime.Now.AddDays(4),
                    TimeSlot = "01:00 PM"
                },
                new Appointment
                {
                    AppointmentId = 5,
                    Patient = PatientDb.Patients[0],
                    Doctor = DoctorDb.Doctors[4],
                    ScheduledDate = DateTime.Now.AddDays(5),
                    TimeSlot = "02:00 PM"
                },
                new Appointment
                {
                    AppointmentId = 6,
                    Patient = PatientDb.Patients[1],
                    Doctor = DoctorDb.Doctors[5],
                    ScheduledDate = DateTime.Now.AddDays(6),
                    TimeSlot = "03:00 PM"
                },
                new Appointment
                {
                    AppointmentId = 7,
                    Patient = PatientDb.Patients[2],
                    Doctor = DoctorDb.Doctors[6],
                    ScheduledDate = DateTime.Now.AddDays(7),
                    TimeSlot = "04:00 PM"
                },
                new Appointment
                {
                    AppointmentId = 8,
                    Patient = PatientDb.Patients[3],
                    Doctor = DoctorDb.Doctors[7],
                    ScheduledDate = DateTime.Now.AddDays(8),
                    TimeSlot = "05:00 PM"
                },
                new Appointment
                {
                    AppointmentId = 9,
                    Patient = PatientDb.Patients[0],
                    Doctor = DoctorDb.Doctors[8],
                    ScheduledDate = DateTime.Now.AddDays(9),
                    TimeSlot = "09:00 AM"
                },
                new Appointment
                {
                    AppointmentId = 10,
                    Patient = PatientDb.Patients[1],
                    Doctor = DoctorDb.Doctors[0],
                    ScheduledDate = DateTime.Now.AddDays(10),
                    TimeSlot = "10:30 AM"
                }
            };
        }
    }
}