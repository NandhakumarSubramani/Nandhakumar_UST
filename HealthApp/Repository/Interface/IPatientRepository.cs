using HealthApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.Repository.Interface
{
    public interface IPatientRepository
    {
        void Add(Patient patient);
        List<Patient> GetAll();
        Patient GetById(int id);
        Patient UpdatePatient(int id, Patient patient);
    }
}
