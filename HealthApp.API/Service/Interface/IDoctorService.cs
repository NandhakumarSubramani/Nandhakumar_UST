using HealthApp.Shared.DTOs;
using HealthApp.Shared.Constant;
using System.Collections.Generic;

namespace HealthApp.API.Service.Interface
{
    public interface IDoctorService
    {
        void AddDoctor(DoctorDto dto);
        List<DoctorDto> GetAllDoctors();
        DoctorDto GetDoctorById(int id);
        List<DoctorDto> SearchBySpecialisation(SpecialisationType specialisation);
        void ChangeDoctorStatus(int id);
    }
}