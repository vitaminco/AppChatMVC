using Microsoft.Build.Framework;

namespace EShopMVCNet7.ViewModels.Account
{
    public class LoginVM
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}
