using Contest.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Contest.Client.Services
{
    public class StudentTutorialService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;

        public StudentTutorialService(NavigationManager navigationManager)
        {
            _httpClient = new HttpClient();
            _navigationManager = navigationManager;
            _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
        }

        public async Task<List<StudentTutorial>?> GetStudentTutorialAsync()
        {
            var StudentTutorials = await _httpClient.GetFromJsonAsync<List<StudentTutorial>>("api/StudentTutorials");
            return StudentTutorials;
        }

        public async Task<List<StudentTutorial>?> GetStudentTutorialByTutorialIdAsync(int tutorialId)
        {
            var url = new UriBuilder(_navigationManager.BaseUri)
            {
                Path = "api/StudentTutorials",
                Query = $"tutorialId={tutorialId}"
            }.ToString();
            var result = await _httpClient.GetFromJsonAsync<List<StudentTutorial>>(url);
            return result;
        }

        public async Task<List<StudentTutorial>?> GetStudentTutorialByTutorialIdAsync(Guid studentId, int tutorialId)
        {
            var url = new UriBuilder(_navigationManager.BaseUri)
            {
                Path = "api/StudentTutorials",
                Query = $"tutorialId={tutorialId}&studentId={studentId.ToString()}"
            }.ToString();
            var result = await _httpClient.GetFromJsonAsync<List<StudentTutorial>>(url);
            return result;
        }

        public async Task<StudentTutorial?> GetStudentTutorialAsync(int studentTutorialId)
        {
            string endpoint = $"api/StudentTutorials/{studentTutorialId}";
            var StudentTutorials = await _httpClient.GetFromJsonAsync<StudentTutorial>(endpoint);
            return StudentTutorials;
        }

        public async Task<bool> CreateStudentTutorialAsync(StudentTutorial StudentTutorials)
        {
            var response = await _httpClient.PostAsJsonAsync("api/StudentTutorials", StudentTutorials);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateStudentTutorialAsync(StudentTutorial StudentTutorials)
        {
            string endpoint = $"api/StudentTutorials/{StudentTutorials.StudentTutorialId}";
            var response = await _httpClient.PutAsJsonAsync(endpoint, StudentTutorials);
            return response.IsSuccessStatusCode;
        }

        public async Task DeleteStudentTutorialAsync(int studentTutorialId)
        {
            string endpoint = $"api/StudentTutorials/{studentTutorialId}";
            await _httpClient.DeleteAsync(endpoint);
        }

    }
}

