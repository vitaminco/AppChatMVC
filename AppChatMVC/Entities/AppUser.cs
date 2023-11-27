using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AppChatMVC.Entities
{
    //làm cho giá trị đc nập vào ko trùng nhau
    [Index("Username", IsUnique = true)]
    public class AppUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")] //bên dưới nó sẽ đc cấu hình not null
        [MaxLength(100)] // độ dài của kí tự
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [MaxLength(200)]
        [MinLength(4, ErrorMessage = "Mật khẩu chưa đủ mạnh!")]
        public string Password { get; set; }
        [NotMapped]//ko tạo cột cfmPassword vào trong bảng database
        [MinLength(4, ErrorMessage = "Mật khẩu chưa đủ mạnh!")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Cfm_Password { get; set; }
        public string Avatar { get; set; }
        [Required(ErrorMessage = "Tên hiển thị là bắt buộc")]
        public string DisplayName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
