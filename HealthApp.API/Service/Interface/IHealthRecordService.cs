using HealthApp.Shared.DTOs;
using System.Collections.Generic;

namespace HealthApp.API.Service.Interface
{
    public interface IHealthRecordService
    {
        void AddRecord(HealthRecordDto dto);
        List<HealthRecordDto> GetAllRecords();
        List<HealthRecordDto> GetPatientRecords(int patientId);
        List<HealthRecordDto> GetHealthRecordsByDoctor(int doctorId, int patientId);
    }
}