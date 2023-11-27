
using AppChatMVC.Common;
using AppChatMVC.Entities;
using EShopMVCNet7.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using AppChatMVC.ViewModels.Account;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace AppChatMVC.Controllers
{
    public class AccountController : ChattingAppControllerBase
    {
        public AccountController(AppChatDbContext db) : base(db)
        {
        }
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(RegisterVM userVM, [FromServices] IWebHostEnvironment env)
        {
            if (ModelState.IsValid == false)
            {
                return View(userVM);
            }
            //chuẩn hóa username và DisplayName
            userVM.Username = userVM.Username.ToLower().Trim();
            userVM.DisplayName = userVM.DisplayName.ToLower().Trim();
            //check username và DisplayName đã tồn tại chưa
            var exists = _db.AppUsers.Any(u => u.DisplayName == userVM.DisplayName || u.Username == userVM.Username);
            if (exists)
            {
                ModelState.AddModelError("", "DisplayName hoặc tên đăng nhập đã được sử dụng!");
                return View(userVM);
            }
            var user = new AppUser
            {
                Username = userVM.Username,
                Password = userVM.Password,
                DisplayName = userVM.DisplayName,
                CreatedAt = DateTime.Now,
            };
            //hình ảnh(CoverImgEven)
            user.Avatar = UploadFile(userVM.Avatar, env.WebRootPath);
            //hash mật khẩu
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            try
            {
                _db.AppUsers.Add(user);
                _db.SaveChanges();
                SetSuccessMesg("Đăng kí thành công");
            }
            catch (Exception ex)
            {
                SetErrorMesg("Lõi nè!!! Vui lòng kiểm tra lại.");
            }
            return RedirectToAction(nameof(Login));//nameof là để ở trên đổi tên thì dưới nó cx đổi theo
        }

        //Đăng nhập
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid == false)
            {
                ModelState.AddModelError("", "Dữ liệu không hợp lệ");
                return View();
            }
            var user = _db.AppUsers.SingleOrDefault(u => u.Username == loginVM.Username);
            //check user
            if (user == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ!!!");
                return View();
            }
            //check mật khẩu
            if (BCrypt.Net.BCrypt.Verify(loginVM.Password, user.Password) == false)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ!!!");
                return View();
            }
            HttpContext.SetUserId(user.Id);
            HttpContext.SetUserName(user.Username);
            HttpContext.SetDislayName(user.DisplayName);

            return RedirectToAction("Index", "Home");
        }
        //Đăng xuất
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        //LOAD FILE
        private string UploadFile(IFormFile file, string dir)
        {
            var fName = file.FileName;
            fName = Path.GetFileNameWithoutExtension(fName)
                + DateTime.Now.Ticks
                + Path.GetExtension(fName);
            //gán giá trị cho cột CoverImg
            var res = "/upload/" + fName;
            //Tạo đường dẫn tuyệt đối( vd: E:/Project/wwwroot/upload/xxx.jpg
            fName = Path.Combine(dir, "upload", fName);
            //Tạo stream để lưu file
            var stream = System.IO.File.Create(fName);
            file.CopyTo(stream);
            stream.Dispose(); //giải phóng bộ nhớ

            return res;
        }
    }
}
