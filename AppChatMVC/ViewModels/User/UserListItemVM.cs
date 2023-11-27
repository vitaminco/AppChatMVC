namespace AppChatMVC.ViewModels.User
{
    public class UserListItemVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
