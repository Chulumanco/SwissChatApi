﻿using System.Text.Json.Serialization;

namespace SwissChatApi.Model.Contacts
{
    public class ContactResponse
    {
        public Guid Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Username { get; set; }
      //  public string Status { get; set; }
        
        public bool IsAuthenticated { get; set; }
      
    }
}
