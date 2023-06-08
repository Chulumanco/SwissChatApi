namespace SwissChatClient.Models
{
    public class ChatsViewModel
    {
       
            public class ReceiveMessageModel
            {
                public List<SendMessageModel> Messages { get; set; } = new List<SendMessageModel>();
            }
            public class SendMessageModel
            {
                public string? UserName { get; set; }
                public string? Message { get; set; }
            }
        
        public string UserDetails { get; set; } = new string("");
    }   
}
