using SwissChatApi.Entities;
using SwissChatApi.Model.Contacts;
using SwissChatApi.Model.Users;

namespace SwissChatApi.Services
{
    public interface IContactService
    {

        Task<Contacts> Create(CreateRequest model);
        Task<IEnumerable<ContactResponse>> GetUserContact(Guid id);
        //  Task<IEnumerable<>> GetUserContact(Guid id);
        Task<Contacts> RemoveContact(string username, Guid userId);
        Task<User> GetUserByID(string userName);




    }
}
