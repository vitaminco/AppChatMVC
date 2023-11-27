namespace AppChatMVC.Entities
{
    public class AppAddFriendTicket
    {
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public int? TargetId { get; set; }
        public string Message { get; set; }
        public bool? IsAccept { get; set; }
        public DateTime SendAt { get; set; }
    }
}
