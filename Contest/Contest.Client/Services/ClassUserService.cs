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

        public async Task<List<ClassUserResponse>?> GetClassUserAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ClassUserResponse>>("api/ClassUsers");
            return result;
        }

        public async Task<List<ClassUserResponse>?> GetClassUserByClassIdAsync(int classId)
        {
            var url = new UriBuilder(_navigationManager.BaseUri)
            {
                Path = "api/ClassUsers",
                Query = $"classId={classId}"
            }.ToString();
            var result = await _httpClient.GetFromJsonAsync<List<ClassUserResponse>>(url);
            return result;
        }

        public async Task<ClassUser?> GetClassUserAsync(int classUserId)
        {
            string endpoint = $"api/ClassUsers/{classUserId}";
            var classUser = await _httpClient.GetFromJsonAsync<ClassUser>(endpoint);
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
