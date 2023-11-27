namespace AppChatMVC.Entities
{
    public class AppMessage
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public int SenderId { get; set; }
        public long ConversationId { get; set; }
        public bool HasAttachment { get; set; }
        public DateTime SendAt { get; set; }
    }
}
