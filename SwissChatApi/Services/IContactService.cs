using SwissChatApi.Entities;
using SwissChatApi.Model.Users;

namespace SwissChatApi.Services
{
    public interface IContactService
    {

        Task<Contacts> Create(string username);
        Task<IEnumerable<Contacts>> GetUserContact(Guid id);
        Task<Contacts> RemoveContact(string username, Guid userId);


        }
}
