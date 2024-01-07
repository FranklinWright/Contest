using Contest.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Contest.Client.Services
{
    public class ClassService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;

        public ClassService(NavigationManager navigationManager)
        {
            _httpClient = new HttpClient();
            _navigationManager = navigationManager;
            _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
        }

        public async Task<List<Class>?> GetClassAsync()
        {
            var Classes = await _httpClient.GetFromJsonAsync<List<Class>>("api/Classes");
            return Classes;
        }

        public async Task<Class?> GetClassAsync(int classId)
        {
            string endpoint = $"api/Classes/{classId}";
            var Classes = await _httpClient.GetFromJsonAsync<Class>(endpoint);
            return Classes;
        }

        public async Task<bool> CreateClassAsync(Class Classes)
        {
            Classes.ClassCode = Classes.ClassCode!.ToUpper();
            var response = await _httpClient.PostAsJsonAsync("api/Classes", Classes);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClassAsync(Class Classes)
        {
            Classes.ClassCode = Classes.ClassCode!.ToUpper();
            string endpoint = $"api/Classes/{Classes.ClassId}";
            var response = await _httpClient.PutAsJsonAsync(endpoint, Classes);
            return response.IsSuccessStatusCode;
        }

        public async Task DeleteClassAsync(int classId)
        {
            string endpoint = $"api/Classes/{classId}";
            await _httpClient.DeleteAsync(endpoint);
        }

    }
}
