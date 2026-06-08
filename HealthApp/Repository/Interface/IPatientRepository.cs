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
        List<Patient> GetAll();
        Patient GetById(int id);
        void Add(Patient patient);
        void UpdatePatient(int id, Patient patient);

    }
}
