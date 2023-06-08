
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwissChatClient.Helpers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public static class ApiHelper
{
    private static readonly HttpClient httpClient = new HttpClient();

    public static async Task<string> GetAsync(string apiUrl, string?  reqtype)
    {
        try
        {
            if(reqtype!=null )
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", reqtype);
                //httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {reqtype}");
                //httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {reqtype}");
            }
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.SendAsync(request);
           // response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        catch (AppException ex)
        {
            // Handle any exceptions or error conditions here
         //   Console.WriteLine($"API call failed: {ex.Message}");
            return ex.Message;
        }
    }
  

    public static async Task<string> PostAsync<T>(string apiUrl, T data, string? reqtype)
    {
        try
        {
            if (reqtype != null)
            {
                //httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {reqtype}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", reqtype);
            }
          var jsonContent = JsonConvert.SerializeObject(data);
           
            HttpContent httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
          // var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

           var response = await httpClient.PostAsync(apiUrl, httpContent);


            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        catch (AppException ex)
        {
            // Handle any exceptions or error conditions here
            return ex.Message;
          //  throw;
        }
    }
}
