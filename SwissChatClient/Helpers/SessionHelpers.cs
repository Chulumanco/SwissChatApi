using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Reflection;
using System.Text.Json;
using System.Web;
using static SwissChatClient.Models.UserViewModel;

namespace SwissChatClient.Helpers
{
    public class SessionHelpers : ISessionHelpers
    {


        public void SetListParameterInSession(HttpContext context, string parameterName, List<string> parameterValue)
        {
            string parameterJson = JsonConvert.SerializeObject(parameterValue);
            byte[] parameterBytes = System.Text.Encoding.UTF8.GetBytes(parameterJson);
            context.Session.Set(parameterName, parameterBytes);
        }
        public List<string> GetListParameterFromSession(HttpContext context, string parameterName)
        {
            if (context.Session.TryGetValue(parameterName, out byte[] parameterBytes))
            {
                string parameterJson = System.Text.Encoding.UTF8.GetString(parameterBytes);
                List<string> parameterList = JsonConvert.DeserializeObject<List<string>>(parameterJson);
                return parameterList;
            }

            return null;
        }
        public void SetStringSessionValue(HttpContext context, string key, string value)
        {
            context.Session.SetString(key, value);
        }

        public string GetStringSessionValue(HttpContext context, string key)
        {
            return context.Session.GetString(key);
        }

    }

}
