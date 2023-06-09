
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class ApiHelper
{
    private readonly HttpClient _httpClient;

    public ApiHelper()
    {
        _httpClient = new HttpClient();
    }

    public async Task<(string ResponseContent, HttpStatusCode StatusCode)> PostAsync<T>(string url, T data, string bearerToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, data);
        string responseContent = await response.Content.ReadAsStringAsync();

        return (responseContent, response.StatusCode);
    }
    public async Task<(string ResponseContent, HttpStatusCode StatusCode)> PostAsync<T>(string url, T data)
    {
        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, data);
        string responseContent = await response.Content.ReadAsStringAsync();

        return (responseContent, response.StatusCode);
    }
    public async Task<(string ResponseContent, HttpStatusCode StatusCode)> GetAsync(string url, string bearerToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

        HttpResponseMessage response = await _httpClient.GetAsync(url);
        string responseContent = await response.Content.ReadAsStringAsync();

        return (responseContent, response.StatusCode);
    }
}
