using Contest.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Contest.Client.Services
{
    public class SecretQuestionsService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;

        public SecretQuestionsService(NavigationManager navigationManager)
        {
            _httpClient = new HttpClient();
            _navigationManager = navigationManager;
            _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
        }

        public async Task<List<SecretQuestion>?> GetSecretQuestionsAsync()
        {
            var secretQuestions = await _httpClient.GetFromJsonAsync<List<SecretQuestion>>("api/SecretQuestions");
            return secretQuestions;
        }

        public async Task<SecretQuestion?> GetSecretQuestionAsync(int secretQuestionId)
        {
            string endpoint = $"api/SecretQuestions/{secretQuestionId}";
            var secretQuestion = await _httpClient.GetFromJsonAsync<SecretQuestion>(endpoint);
            return secretQuestion;
        }

        public async Task<SecretQuestion?> CreateSecretQuestionAsync(SecretQuestion secretQuestion)
        {
            var response = await _httpClient.PostAsJsonAsync("api/SecretQuestions", secretQuestion);
            if (response.IsSuccessStatusCode)
            {
                var newSecretQuestion = await response.Content.ReadFromJsonAsync<SecretQuestion>();
                return newSecretQuestion;
            }
            else
            {
                return null;
            }
        }

        public async Task<SecretQuestion?> UpdateSecretQuestionAsync(SecretQuestion secretQuestion)
        {
            string endpoint = $"api/SecretQuestions/{secretQuestion.SecretQuestionId}";
            var response = await _httpClient.PutAsJsonAsync(endpoint, secretQuestion);
            if (response.IsSuccessStatusCode)
            {
                var updatedSecretQuestion = await response.Content.ReadFromJsonAsync<SecretQuestion>();
                return updatedSecretQuestion;
            }
            else
            {
                return null;
            }
        }
    }
}
