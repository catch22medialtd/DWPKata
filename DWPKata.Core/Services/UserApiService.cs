using DWPKata.Core.Dtos;
using DWPKata.Core.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DWPKata.Core.Services
{
    public class UserApiService : IUserApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserServiceAPI");
        }

        public async Task<List<UserDto>> GetUsersFromLondon()
        {
            return await DoGetUsers("/city/London/users");
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            return await DoGetUsers("/users");
        }

        private async Task<List<UserDto>> DoGetUsers(string requestUri)
        {
            var httpResponseMessage = await _httpClient
                .GetAsync(requestUri);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserDto>>(response);

            return users;
        }
    }
}