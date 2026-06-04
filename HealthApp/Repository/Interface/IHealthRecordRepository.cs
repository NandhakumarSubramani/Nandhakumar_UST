using HealthApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.Repository.Interface
{
    public interface IHealthRecordRepository
    {
        void Add(HealthRecord record);
        List<HealthRecord> GetAll();
    }
}