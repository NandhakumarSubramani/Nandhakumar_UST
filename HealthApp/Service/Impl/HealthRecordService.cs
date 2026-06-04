using HealthApp.Database;
using HealthApp.Models;
using HealthApp.Repository.Interface;
using HealthApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace HealthApp.Service.Impl
{
    public class HealthRecordService : IHealthRecordService
    {
        private readonly IHealthRecordRepository _repo;

        public HealthRecordService(IHealthRecordRepository repo)
        {
            _repo = repo;
        }

        public void AddRecord(HealthRecord record)
        {
            var all = _repo.GetAll();
            record.RecordId = all.Any()? all.Max(r => r.RecordId) + 1: 1;
            _repo.Add(record);
        }

        public List<HealthRecord> GetAllRecords()
        {
            return _repo.GetAll();
        }

        public List<HealthRecord> GetPatientRecords(int patientId)
        {
            var all = _repo.GetAll();

            return all.Where(r => r.Patient != null && r.Patient.PatientId == patientId).ToList();
        }

        public List<HealthRecord> GetHealthRecordsByDoctor(int doctorId, int patientId)
        {
            var all = _repo.GetAll();

            return all.Where(r => r.Doctor != null
                         && r.Patient != null
                         && r.Doctor.DoctorId == doctorId
                         && r.Patient.PatientId == patientId)
                .OrderByDescending(r => r.VisitDate).ToList();
        }
    }
}