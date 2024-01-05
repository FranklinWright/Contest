using Contest.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Contest.Client.Services
{
    public class QuizService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;

        public QuizService(NavigationManager navigationManager)
        {
            _httpClient = new HttpClient();
            _navigationManager = navigationManager;
            _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
        }

        public async Task<List<Quiz>?> GetQuizAsyncInOrder(int tutorialId)
        {
            try
            {
                var quiz = await _httpClient.GetFromJsonAsync<List<Quiz>>("api/Quizs");
                if (quiz != null)
                {
                    var filteredQuizs = quiz.Where(quiz => quiz.TutorialId == tutorialId).OrderBy(quiz => quiz.Order).ToList();
                    return filteredQuizs;
                }

                return quiz;
            }
            catch (HttpRequestException ex)
            {
                var status = ex.StatusCode.ToString();
                throw;
            }
        }

        public async Task<List<Quiz>?> GetQuizAsync()
        {
            var quiz = await _httpClient.GetFromJsonAsync<List<Quiz>>("api/quizs");
            return quiz;
        }

        public async Task<Quiz?> GetQuizAsync(int quizId)
        {
            string endpoint = $"api/quizs/{quizId}";
            var quiz = await _httpClient.GetFromJsonAsync<Quiz>(endpoint);
            return quiz;
        }

        public async Task<bool> GetQuizAsync(Quiz quiz)
        {
            var response = await _httpClient.PostAsJsonAsync("api/quizs", quiz);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateQuizAsync(Quiz quiz)
        {
            string endpoint = $"api/quizs/{quiz.QuizId}";
            var response = await _httpClient.PutAsJsonAsync(endpoint, quiz);
            return response.IsSuccessStatusCode;
        }

        public async Task DeleteQuizAsync(int quizId)
        {
            string endpoint = $"api/quizs/ {quizId}";
            await _httpClient.DeleteAsync(endpoint);
        }

        public async Task<bool> CreateQuizAsync(Quiz quiz)
        {
            var response = await _httpClient.PostAsJsonAsync("api/quizs", quiz);
            return response.IsSuccessStatusCode;
        }

    }
}