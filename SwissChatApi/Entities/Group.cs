namespace SwissChatApi.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
    }
}
