using System.ComponentModel.DataAnnotations;

namespace SwissChatClient.Models
{
    public class UserViewModel
    {
        public class RegisterRequest
        {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public class Authenticate
        {
            [Required(ErrorMessage = "Username is required")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    
        public class AuthenticateResponse
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Token { get; set; }
            public bool IsAuthenticated { get; set; }
        }
        public class JsonUserResponse
        {
            public string Message { get; set; }
            public bool? IsSuccess { get; set; }
        }
        public enum UserSession
        {
            Id =1,
            FirstName,
            LastName,
            Username,
            Token,
            IsAuthenticated
        }

    }
}
