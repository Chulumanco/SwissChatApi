
using System.Security.Cryptography;

namespace SwissChatClient.Helpers
{
    public interface ISessionHelpers
    {
        void SetList<T>(ISession session, string key, List<T> list);
        List<T> GetList<T>(ISession session, string key);
        void Set<T>(ISession session, string key, T value);
        T Get<T>(ISession session, string key);


        
       

    } 
}
