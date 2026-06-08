using HealthApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.API.Service.Interface
{
    public interface IDoctorService
    {
        void AddDoctor(Doctor doctor);
        List<Doctor> GetAllDoctors();
        Doctor GetDoctorById(int id);

        List<Doctor> SearchBySpecialisation(SpecialisationType specialisation);
        void ChangeDoctorStatus(int id);
    }
}
