
using System.Security.Cryptography;

namespace SwissChatClient.Helpers
{
    public interface ISessionHelpers
    {

        void SetListParameterInSession(HttpContext context, string parameterName, List<string> parameterValue);
        List<string> GetListParameterFromSession(HttpContext context, string parameterName);
        void SetStringSessionValue(HttpContext context, string key, string value);


       string GetStringSessionValue(HttpContext context, string key);






    } 
}
