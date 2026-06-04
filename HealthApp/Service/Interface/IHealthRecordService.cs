using HealthApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.Service.Interface
{
    public interface IHealthRecordService
    {
        void AddRecord(HealthRecord record);
        List<HealthRecord> GetAllRecords();
        List<HealthRecord> GetPatientRecords(int patientId);
        List<HealthRecord> GetHealthRecordsByDoctor(int doctorId, int patientId);

    }
}
