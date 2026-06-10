using HealthApp.Repository.Impl;
using HealthApp.Repository.Interface;
using HealthApp.Service.Impl;
using HealthApp.Service.Interface;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace HealthApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IPatientRepository,PatientRepository>();
            container.RegisterType<IPatientApiService, PatientApiService>();
            container.RegisterType<IDoctorRepository, DoctorRepository>();
            container.RegisterType<IDoctorApiService, DoctorApiService>();
            container.RegisterType<IHealthRecordRepository, HealthRecordRepository>();
            container.RegisterType<IHealthRecordApiService, HealthRecordApiService>();
            container.RegisterType<IAppointmentRepository, AppointmentRepository>();
            container.RegisterType<IAppointmentApiService, AppointmentApiService>();


            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}