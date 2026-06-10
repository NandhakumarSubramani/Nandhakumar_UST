using AutoMapper;
using HealthApp.API.Data;
using HealthApp.API.Repository.Interface;
using HealthApp.API.Service.Interface;
using HealthApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthApp.API.Service.Impl
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // CREATE
        public void RegisterPatient(PatientDto dto)
        {
            if (dto == null)
                throw new Exception("Patient cannot be null");

            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new Exception("Patient name is required");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new Exception("Email is required");

            var patients = _repo.GetAll();

            bool emailExists = patients.Any(p =>
                p.Email.ToLower() == dto.Email.ToLower());

            if (emailExists)
                throw new Exception("Email already exists");

            var patient = _mapper.Map<Patient>(dto);

            patient.CreatedDate = DateTime.Now;

            _repo.Add(patient);
        }

        // GET ALL
        public List<PatientDto> GetAll()
        {
            var list = _repo.GetAll();
            return _mapper.Map<List<PatientDto>>(list);
        }

        // GET BY ID
        public PatientDto GetPatientById(int id)
        {
            var patient = _repo.GetById(id);

            if (patient == null)
                throw new Exception($"Patient with id {id} not found");

            return _mapper.Map<PatientDto>(patient);
        }

        // UPDATE
        public void UpdatePatientById(int id, PatientDto dto)
        {
            var existingPatient = _repo.GetById(id);

            if (existingPatient == null)
                throw new Exception($"Patient with id {id} not found");

            var patient = _mapper.Map<Patient>(dto);

            patient.PatientId = id;

            _repo.UpdatePatient(id, patient);
        }
    }
}