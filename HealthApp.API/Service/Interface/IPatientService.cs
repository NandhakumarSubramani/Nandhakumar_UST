using HealthApp.API.Models;
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
        string UpdatePatientById(int id,Patient patient);
    }
}
