using HealthApp.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HealthApp.API.Service.Interface
{
    public interface IPatientService
    {
        void RegisterPatient(Patient patient);
        Patient GetPatientById(int id);
        List<Patient> GetAll();
        void UpdatePatientById(int id, Patient patient);
    }
}