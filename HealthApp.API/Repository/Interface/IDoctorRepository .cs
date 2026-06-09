using HealthApp.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.API.Repository.Interface
{
    public interface IDoctorRepository
    {
        void Add(Doctor doctor);
        List<Doctor> GetAll();
        Doctor GetById(int id);

        void Update(Doctor doctor);
    }
}
