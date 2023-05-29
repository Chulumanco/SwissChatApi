using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SwissChatApi.Authorization;
using SwissChatApi.Entities;
using SwissChatApi.Helpers;
using SwissChatApi.Model.Contacts;
using SwissChatApi.Model.Users;

namespace SwissChatApi.Services
{
    public class ContactService: IContactService
    {
        private SwissDBContext _context;
        public ContactService(SwissDBContext context)
        {
            _context = context;
          
        }
        public async Task<Contacts> Create(string username)
        {
            var result = await GetUserByID(username);
            var contact = new Contacts();
            if (result == null)
            {
                throw new AppException("User not found, please make sure that the given Username is correct");
                // return contact;
            }

            var checkDuplicate = await UserContactExists(username, result.Id);
            if (checkDuplicate)
            {
                throw new AppException("You already have this contact as a mutual");
            }
            contact.Username = username;
            contact.UserId = result.Id;
            contact.Status = "Saved";
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }
        public async Task <IEnumerable<Contacts>> GetUserContact(Guid id)
        {
           var users =   await _context.Contacts.Where(x=>x.UserId==id && x.Status=="Saved").ToListAsync(); 
            if(users.Count<0)
            {
                throw new AppException("No contacts add contacts");
            }
            return users;

        
        }
        public  IEnumerable<Contacts> GetAll()
        {
            return  _context.Contacts;
        }
        private async Task<bool> UserContactExists(string username,Guid userId)
        {
            var user = await _context.Contacts.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() &&x.UserId ==userId );
            if (user != null)
            {
                return true;
            }
           return false;
        }
        private async Task<User> GetUserByID(string userName)
        {
          var user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == userName.ToLower());
            //if (user == null) throw new KeyNotFoundException("User not found, please make sure that the given Username is correct");
            return user;
            
        }
       public async Task<Contacts> RemoveContact(string username,Guid userId)
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
