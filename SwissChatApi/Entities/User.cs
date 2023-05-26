using System.Text.Json.Serialization;

namespace SwissChatApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
