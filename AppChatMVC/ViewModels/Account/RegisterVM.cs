using AppChatMVC.Entities;
using System.ComponentModel.DataAnnotations;
namespace AppChatMVC.ViewModels.Account
{
    public class RegisterVM
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        public IFormFile Avatar { get; set; }
        [Required]public string DisplayName { get; set; }
    }
}