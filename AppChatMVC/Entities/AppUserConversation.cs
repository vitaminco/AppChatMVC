namespace AppChatMVC.Entities
{
    public class AppUserConversation
    {
        public long Id { get; set; }
        public int? UserId { get; set; }
        public long? ConversationId { get; set; }
    }
}
