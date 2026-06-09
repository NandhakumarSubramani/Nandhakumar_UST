using HealthApp.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.API.Repository.Interface
{
    public interface IHealthRecordRepository
    {
        void Add(HealthRecord record);
        List<HealthRecord> GetAll();
    }
}