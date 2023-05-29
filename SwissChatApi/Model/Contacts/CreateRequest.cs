using System.ComponentModel.DataAnnotations;

namespace SwissChatApi.Model.Contacts
{
    public class CreateRequest
    {
        [Required]
        public string Username { get; set; }
    }
}
