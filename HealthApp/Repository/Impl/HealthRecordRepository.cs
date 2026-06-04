using HealthApp.Database;
using HealthApp.Models;
using HealthApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.Repository.Impl
{
    public class HealthRecordRepository : IHealthRecordRepository
    {
        public void Add(HealthRecord record)
        {
            HealthRecordDb.Records.Add(record);
        }
        public List<HealthRecord> GetAll()
        {
            return HealthRecordDb.Records;
        }
    }
}