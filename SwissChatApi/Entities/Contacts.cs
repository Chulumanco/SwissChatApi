namespace SwissChatApi.Entities
{
    public class Contacts
    {
        public Guid Id { get; set; }    
        public string Username { get; set; }
        //The status of a contact to a user
        public string Status { get; set; }
        public Guid UserId { get; set; }
        public User Users { get; set; }
    }
}
