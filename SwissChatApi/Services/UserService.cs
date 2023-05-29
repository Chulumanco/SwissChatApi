using AutoMapper;
using BCrypt.Net;
using SwissChatApi.Authorization;
using SwissChatApi.Entities;
using SwissChatApi.Helpers;
using SwissChatApi.Model.Users;

namespace SwissChatApi.Services
{
    public class UserService : IUserService
    {
        private SwissDBContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        public UserService(
            SwissDBContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
               throw new AppException("Username or password is incorrect");

            // authentication successful

            var response = _mapper.Map<AuthenticateResponse>(user);
            await UpdateUserAuth(response.Id,"login");
            response.Token = _jwtUtils.GenerateToken(user);
           // response.IsAuthenticated = true;

            return response;
        }
        public async Task<string> Logoff(Guid id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);
                 await UpdateUserAuth(user.Id, null);
                // response.IsAuthenticated = true;

            return "User logged out";
        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }
        public User GetById(Guid id)
        {
            return getUser(id);
        }
        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<User>(model);
            user.Status = "Active";

            // hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save user
            
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void Update(Guid id, UpdateRequest model)
        {
            var user = getUser(id);

            // validate
            if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

           

            // copy model to user and save
            _mapper.Map(model, user);
          
          var changeIsSucces =  _context.SaveChanges();
            if(changeIsSucces>0)
                UpdateUserContact(user.Id);
        }
        public void Delete(Guid id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        // helper methods

        private User getUser(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
        private Contacts getContact(Guid id)
        {
            var user = _context.Contacts.FirstOrDefault(x => x.UserId == id);   
            //if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
        private void UpdateUserContact(Guid id)
        {
            var contact = getContact(id);
            if(contact ==null)
            {
                //Don't do anything
                return ;
            }
            _context.Contacts.Update(contact);
            _context.SaveChanges();
           // return contact;

        }
        private async Task<int> UpdateUserAuth(Guid id, string? action)
        {
            /// var contact = getContact(id);
            var user = await _context.Users.FindAsync(id);
            if (action == "login")
            {
                user.IsAuthenticated = true;
            }
            else
                user.IsAuthenticated = false;
            _context.Update(user);
            var resap = await  _context.SaveChangesAsync();
           return resap;

        }
    }
}
