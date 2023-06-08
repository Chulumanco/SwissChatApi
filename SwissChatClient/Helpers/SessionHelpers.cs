using Microsoft.AspNetCore.Mvc;
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
        public T Get<T>(ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

        public void Set<T>(ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public void SetList<T>(ISession session, string key, List<T> list)
        {
            session.SetString(key, JsonSerializer.Serialize(list));

        }

        public  List<T> GetList<T>(ISession session, string key)
        {
            var value = session.GetString(key);
          
            return value == null ? new List<T>() : JsonSerializer.Deserialize<List<T>>(value);
        }
      

    }

}
