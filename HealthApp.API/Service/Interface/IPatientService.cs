using HealthApp.API.Data;
using HealthApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HealthApp.API.Service.Interface
{

    public interface IPatientService
    {
        void RegisterPatient(PatientDto patientDto);
        PatientDto GetPatientById(int id);
        List<PatientDto> GetAll();
        void UpdatePatientById(int id, PatientDto patientDto);
    }

}