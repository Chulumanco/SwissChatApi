using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace SwissChatClient.Helpers
{
    public class ChatApiHelper: IChatApiHelper
    {
        private readonly HttpClient _httpClient;
        public ChatApiHelper()
        {
            _httpClient = new HttpClient();
        }
        //public async Task<string> GetAsync(string url)
        public async Task<string> PostAsync<T>(string url, T data)
        {

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
        public async Task<string> PostAsync<T>(string url, T data, string? bearerToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
        public async Task<string> GetAsync(string url)
        {
           

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }

        public async Task<string> GetAsync(string url, string bearerToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
        
    }
}


