using HealthApp.Service.Interface;
using HealthApp.Shared.DTOs;
using HealthApp.Shared.Constant;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthApp.Service.Impl
{
    public class DoctorApiService : IDoctorApiService
    {
        private readonly string baseUrl = "https://localhost:44339/api/doctors";

        public async Task<List<DoctorDto>> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                var res = await client.GetAsync(baseUrl);
                if (res.IsSuccessStatusCode)
                    return await res.Content.ReadAsAsync<List<DoctorDto>>();
            }
            return new List<DoctorDto>();
        }

        public async Task<DoctorDto> GetById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var res = await client.GetAsync($"{baseUrl}/{id}");
                if (res.IsSuccessStatusCode)
                    return await res.Content.ReadAsAsync<DoctorDto>();
            }
            return null;
        }

        public async Task Create(DoctorDto dto)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.PostAsJsonAsync(baseUrl, dto);
            }
        }

        public async Task<List<DoctorDto>> SearchBySpecialisation(SpecialisationType type)
        {
            using (HttpClient client = new HttpClient())
            {
                var res = await client.GetAsync($"{baseUrl}/specialisation/{type}");
                if (res.IsSuccessStatusCode)
                    return await res.Content.ReadAsAsync<List<DoctorDto>>();
            }
            return new List<DoctorDto>();
        }

        public async Task ToggleStatus(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.PutAsync($"{baseUrl}/{id}/toggle", null);
            }
        }
    }
}