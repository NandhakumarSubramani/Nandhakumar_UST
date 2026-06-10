using AutoMapper;
using HealthApp.API.Constant;
using HealthApp.API.Data;
using HealthApp.API.Repository.Interface;
using HealthApp.API.Service.Interface;
using HealthApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthApp.API.Service.Impl
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        private readonly IMapper _mapper;

        public DoctorService(IDoctorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // CREATE
        public void AddDoctor(DoctorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new Exception("Doctor name is required");

            var doctor = _mapper.Map<Doctor>(dto);
            _repo.Add(doctor);
        }

        // GET ALL
        public List<DoctorDto> GetAllDoctors()
        {
            var list = _repo.GetAll();
            return _mapper.Map<List<DoctorDto>>(list);
        }

        // GET BY ID
        public DoctorDto GetDoctorById(int id)
        {
            var doctor = _repo.GetById(id);

            if (doctor == null)
                throw new Exception($"Doctor with id {id} not found");

            return _mapper.Map<DoctorDto>(doctor);
        }

        // SEARCH
        public List<DoctorDto> SearchBySpecialisation(SpecialisationType specialisation)
        {
            string spec = specialisation.ToString();

            var result = _repo.GetAll()
                              .Where(d => d.Specialisation == spec)
                              .ToList();

            return _mapper.Map<List<DoctorDto>>(result);
        }

        // TOGGLE STATUS
        public void ChangeDoctorStatus(int id)
        {
            var doctor = _repo.GetById(id);

            if (doctor == null)
                throw new Exception("Doctor not found");

            doctor.IsActive = !(doctor.IsActive ?? false);
            _repo.Update(doctor);
        }
    }
}