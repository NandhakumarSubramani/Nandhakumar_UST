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
    public class HealthRecordService : IHealthRecordService
    {
        private readonly IHealthRecordRepository _repo;
        private readonly IMapper _mapper;

        public HealthRecordService(IHealthRecordRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // CREATE
        public void AddRecord(HealthRecordDto dto)
        {
            if (dto == null)
                throw new Exception("Invalid record");

            var record = _mapper.Map<HealthRecord>(dto);
            _repo.Add(record);
        }

        // GET ALL
        public List<HealthRecordDto> GetAllRecords()
        {
            var list = _repo.GetAll();
            return _mapper.Map<List<HealthRecordDto>>(list);
        }

        // GET BY PATIENT
        public List<HealthRecordDto> GetPatientRecords(int patientId)
        {
            var list = _repo.GetAll()
                            .Where(r => r.PatientId == patientId)
                            .ToList();

            return _mapper.Map<List<HealthRecordDto>>(list);
        }

        // FILTER BY DOCTOR + PATIENT
        public List<HealthRecordDto> GetHealthRecordsByDoctor(int doctorId, int patientId)
        {
            var list = _repo.GetAll()
                            .Where(r => r.DoctorId == doctorId &&
                                        r.PatientId == patientId)
                            .OrderByDescending(r => r.VisitDate)
                            .ToList();

            return _mapper.Map<List<HealthRecordDto>>(list);
        }
    }
}
