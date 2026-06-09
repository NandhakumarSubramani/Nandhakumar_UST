using HealthApp.API.Data;
using HealthApp.API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.API.Repository.Impl
{
    public class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly HealthAppDBEntities _db;
        public void Add(HealthRecord record)
        {
            _db.HealthRecords.Add(record);
            _db.SaveChanges();
        }
        public List<HealthRecord> GetAll()
        {
            return _db.HealthRecords.ToList();
        }
    }
}