using Contest.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Contest.Client.Services
{
    public class ClassTutorialService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;

        public ClassTutorialService(NavigationManager navigationManager)
        {
            _httpClient = new HttpClient();
            _navigationManager = navigationManager;
            _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
        }

        public async Task<List<ClassTutorialResponse>?> GetClassTutorialsAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ClassTutorialResponse>>("api/ClassTutorials");
            return result;
        }

        public async Task<List<ClassTutorialResponse>?> GetClassTutorialsByClassIdAsync(int classId)
        {
            var url = new UriBuilder(_navigationManager.BaseUri)
            {
                Path = "api/ClassTutorials",
                Query = $"classId={classId}"
            }.ToString();
            var result = await _httpClient.GetFromJsonAsync<List<ClassTutorialResponse>>(url);
            return result;
        }

        public async Task<ClassTutorial?> GetClassTutorialAsync(int ClassTutorialId)
        {
            string endpoint = $"api/ClassTutorials/{ClassTutorialId}";
            var ClassTutorial = await _httpClient.GetFromJsonAsync<ClassTutorial>(endpoint);
            return ClassTutorial;
        }

        public async Task<bool> CreateClassTutorialAsync(ClassTutorial ClassTutorial)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ClassTutorials", ClassTutorial);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClassTutorialAsync(Class ClassTutorial)
        {
            string endpoint = $"api/ClassTutorials/{ClassTutorial.ClassId}";
            var response = await _httpClient.PutAsJsonAsync(endpoint, ClassTutorial);
            return response.IsSuccessStatusCode;
        }

        public async Task DeleteClassTutorialAsync(int ClassTutorialId)
        {
            string endpoint = $"api/ClassTutorials/{ClassTutorialId}";
            await _httpClient.DeleteAsync(endpoint);
        }

    }
}
