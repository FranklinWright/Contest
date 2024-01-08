using Contest.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Contest.Client.Services
{
    public class TutorialService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;

        public TutorialService(NavigationManager navigationManager)
        {
            _httpClient = new HttpClient();
            _navigationManager = navigationManager;
            _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
        }

        public async Task<List<TutorialResponse>?> GetTutorialAsync()
        {
                var tutorial = await _httpClient.GetFromJsonAsync<List<TutorialResponse>>("api/Tutorials");
                return tutorial;
        }
        
        public async Task<TutorialResponse?> GetTutorialAsync(int tutorialId)
        {
            string endpoint = $"api/Tutorials/{tutorialId}";
            var tutorial = await _httpClient.GetFromJsonAsync<TutorialResponse>(endpoint);
            return tutorial;
        }

        public async Task<bool> CreateTutorialAsync(Tutorial tutorial)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Tutorials", tutorial);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTutorialAsync(Tutorial tutorial)
        {
            string endpoint = $"api/Tutorials/{tutorial.TutorialId}";
            var response = await _httpClient.PutAsJsonAsync(endpoint, tutorial);
            return response.IsSuccessStatusCode;
        }

        public async Task DeleteTutorialAsync(int tutorialId)
        {
            string endpoint = $"api/Tutorials/{tutorialId}";
            await _httpClient.DeleteAsync(endpoint);
        }

    }
}
