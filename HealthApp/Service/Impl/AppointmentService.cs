using HealthApp.Service.Interface;
using HealthApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthApp.Service.Impl
{
    public class AppointmentApiService : IAppointmentApiService
    {
        private readonly string baseUrl = "https://localhost:44339/api/appointments";

        public async Task<List<AppointmentDto>> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                var res = await client.GetAsync(baseUrl);
                return await res.Content.ReadAsAsync<List<AppointmentDto>>();
            }
        }

        public async Task<AppointmentDto> GetById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var res = await client.GetAsync($"{baseUrl}/{id}");
                return await res.Content.ReadAsAsync<AppointmentDto>();
            }
        }

        public async Task<List<AppointmentDto>> GetByPatient(int patientId)
        {
            using (HttpClient client = new HttpClient())
            {
                var res = await client.GetAsync($"{baseUrl}/patient/{patientId}");
                return await res.Content.ReadAsAsync<List<AppointmentDto>>();
            }
        }

        public async Task Create(AppointmentDto dto)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.PostAsJsonAsync(baseUrl, dto);
            }
        }

        public async Task Confirm(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.PutAsync($"{baseUrl}/{id}/confirm", null);
            }
        }

        public async Task Cancel(int id, string reason)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.PutAsync($"{baseUrl}/{id}/cancel?reason={Uri.EscapeDataString(reason)}", null);
            }
        }

        public async Task<List<string>> CheckAvailability(int doctorId, DateTime date)
        {
            using (HttpClient client = new HttpClient())
            {
                var res = await client.GetAsync(
                    $"{baseUrl}/availability?doctorId={doctorId}&date={date:yyyy-MM-dd}");

                return await res.Content.ReadAsAsync<List<string>>();
            }
        }


        public async Task MarkCompleted(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.PutAsync($"{baseUrl}/{id}/complete", null);
            }
        }

    }
}