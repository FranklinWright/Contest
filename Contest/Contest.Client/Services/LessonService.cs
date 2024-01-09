using Contest.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Contest.Client.Services
{
    public class LessonService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;

        public LessonService(NavigationManager navigationManager)
        {
            _httpClient = new HttpClient();
            _navigationManager = navigationManager;
            _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
        }

        public async Task<List<Lesson>?> GetLessonAsyncInOrder(int tutorialId)
        {
            try
            {
                var url = new UriBuilder(_navigationManager.BaseUri)
                {
                    Path = "api/Lessons",
                    Query = $"tutorialId={tutorialId}"
                }.ToString();
                var result = await _httpClient.GetFromJsonAsync<List<Lesson>>(url);
                return result;
            }
            catch (HttpRequestException ex)
            {
                var status = ex.StatusCode.ToString();
                throw;
            }
        }

        public async Task<List<Lesson>?> GetLessonAsync()
        {
            var lesson = await _httpClient.GetFromJsonAsync<List<Lesson>>("api/lessons");
            return lesson;
        }

        public async Task<Lesson?> GetLessonAsync(int lessonId)
        {
            string endpoint = $"api/lessons/{lessonId}";
            var lesson = await _httpClient.GetFromJsonAsync<Lesson>(endpoint);
            return lesson;
        }

        public async Task<bool> GetLessonAsync(Lesson lesson)
        {
            var response = await _httpClient.PostAsJsonAsync("api/lessons", lesson);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateLessonAsync(Lesson lesson)
        {
            string endpoint = $"api/lessons/{lesson.LessonId}";
            var response = await _httpClient.PutAsJsonAsync(endpoint, lesson);
            return response.IsSuccessStatusCode;
        }

        public async Task DeleteLessonAsync(int lessonId)
        {
            string endpoint = $"api/lessons/ {lessonId}";
            await _httpClient.DeleteAsync(endpoint);
        }

        public async Task<bool> CreateLessonAsync(Lesson lesson)
        {
            var response = await _httpClient.PostAsJsonAsync("api/lessons", lesson);
            return response.IsSuccessStatusCode;
        }

    }
}