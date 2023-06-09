namespace SwissChatClient.Helpers
{
    public interface IChatApiHelper
    {
        Task<string> PostAsync<T>(string url, T data);
        Task<string> PostAsync<T>(string url, T data, string bearerToken);
        Task<string> GetAsync(string url);
        Task<string> GetAsync(string url, string bearerToken);
    


    }
}
