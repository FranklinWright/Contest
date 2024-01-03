using Contest.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Contest.Client.Services
{
    public class AccountTypesService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;

        public AccountTypesService(NavigationManager navigationManager) { 
            _httpClient = new HttpClient();
            _navigationManager = navigationManager;
            _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
        }

        public async Task<List<AccountType>?> GetAccountTypesAsync()
        {
            try
            {
                var accountTypes = await _httpClient.GetFromJsonAsync<List<AccountType>>("api/AccountTypes");
                return accountTypes;
            }
            catch (HttpRequestException ex)
            {
                var status = ex.StatusCode.ToString();
                throw;
            }        
        }
    }
}
