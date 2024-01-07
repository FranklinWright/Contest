using Contest.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Contest.Client.Services
{
    public class ClassUserService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;

        public ClassUserService(NavigationManager navigationManager)
        {
            _httpClient = new HttpClient();
            _navigationManager = navigationManager;
            _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
        }

        public async Task<List<Class>?> GetClassUserAsync()
        {
            var classUser = await _httpClient.GetFromJsonAsync<List<Class>>("api/ClassUsers");
            return classUser;
        }

        public async Task<Class?> GetClassUserAsync(int classUserId)
        {
            string endpoint = $"api/ClassUsers/{classUserId}";
            var classUser = await _httpClient.GetFromJsonAsync<Class>(endpoint);
            return classUser;
        }

        public async Task<bool> CreateClassUserAsync(ClassUser classUser)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ClassUsers", classUser);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClassUserAsync(Class classUser)
        {
            string endpoint = $"api/ClassUsers/{classUser.ClassId}";
            var response = await _httpClient.PutAsJsonAsync(endpoint, classUser);
            return response.IsSuccessStatusCode;
        }

        public async Task DeleteClassUserAsync(int classUserId)
        {
            string endpoint = $"api/ClassUsers/{classUserId}";
            await _httpClient.DeleteAsync(endpoint);
        }

    }
}
