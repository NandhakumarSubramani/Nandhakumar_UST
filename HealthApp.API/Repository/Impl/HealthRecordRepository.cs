using HealthApp.API.Data;
using HealthApp.API.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace HealthApp.API.Repository.Impl
{
    public class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly HealthAppDBEntities _db;

        public HealthRecordRepository()
        {
            _db = new HealthAppDBEntities();
        }

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