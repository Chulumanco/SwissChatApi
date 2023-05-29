using SwissChatApi.Entities;
using SwissChatApi.Model.Users;

namespace SwissChatApi.Services
{
    public interface IUserService
    {
       Task <AuthenticateResponse> Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        void Register(RegisterRequest model);
        void Update(Guid id, UpdateRequest model);
        void Delete(Guid id);
    }
}
