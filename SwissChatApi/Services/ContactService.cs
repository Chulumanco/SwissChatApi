using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SwissChatApi.Authorization;
using SwissChatApi.Entities;
using SwissChatApi.Helpers;
using SwissChatApi.Model.Contacts;
using SwissChatApi.Model.Users;
using System.Reflection.Emit;

namespace SwissChatApi.Services
{
    public class ContactService : IContactService
    {
        private SwissDBContext _context;
        public ContactService(SwissDBContext context)
        {
            _context = context;

        }
        public async Task<Contacts> Create(CreateRequest model)
        {
            var result = await GetUserByID(model.Username.Trim());
            var contact = new Contacts();
            // if (result.Username == null)
            // {
            //     throw new AppException("User not found, please make sure that the given Username is correct");
            //     // return contact;
            // }

            var checkDuplicate = await UserContactExists(model.Username, model.UserId);
            if (checkDuplicate)
            {
                throw new AppException("You already have this contact as a mutual");
            }
            contact.Username = model.Username;
            contact.UserId = model.UserId;
            contact.Status = "Saved";
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }
        public async Task<IEnumerable<ContactResponse>> GetUserContact(Guid id)
        {
            var users = await (from user in _context.Users
                               join con in _context.Contacts on id equals con.UserId
                               where user.Id == con.UserId && con.Status == "Saved"
                               select new ContactResponse { Id = con.UserId, Username = con.Username, IsAuthenticated = user.IsAuthenticated }).ToListAsync();
            // var users =   await _context.Contacts.Where(x=>x.UserId==id && x.Status=="Saved").ToListAsync(); 
            //if(users.<0)
            //{
            //    throw new AppException("No contacts add contacts");
            //}
            return users;


        }

        public IEnumerable<Contacts> GetAll()
        {
            return _context.Contacts;
        }
        private async Task<bool> UserContactExists(string username, Guid userId)
        {
            bool exists = _context.Contacts.Any(x => x.Username.ToLower() == username.ToLower() && x.UserId == userId);

            return exists;
        }
        public async Task<User> GetUserByID(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
            if (user == null) throw new KeyNotFoundException("User not found, please make sure that the given Username is correct");
            return user;

        }
        public async Task<Contacts> RemoveContact(string username, Guid userId)
        {
            var user = await _context.Contacts.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.UserId == userId);

            // validate
            user.Status = "Removed";
            _context.Contacts.Update(user);

            _context.SaveChanges();
            return user;
        }

    }
}
