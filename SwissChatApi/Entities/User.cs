using System.Text.Json.Serialization;

namespace SwissChatApi.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public bool IsAuthenticated { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public ICollection<Contacts> Contacts { get; set; }
        public ICollection<Group> Groups { get; set; }

    }
}
