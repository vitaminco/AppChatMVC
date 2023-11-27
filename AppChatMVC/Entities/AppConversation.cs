namespace AppChatMVC.Entities
{
    public class AppConversation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? LastMessageId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
