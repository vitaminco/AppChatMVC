using AppChatMVC.ViewModels.User;
using AppChatMVC.Entities;
using AppChatMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace AppChatMVC.Controllers
{
    public class HomeController : ChattingAppControllerBase
    {
        public HomeController(AppChatDbContext db) : base(db)
        {
        }

        public IActionResult Index()
        {
            var user = _db.AppUsers
                                .Select(u => new UserListItemVM
                                {
                                    Id = u.Id,
                                    Username = u.Username,
                                    Avatar = u.Avatar,
                                    DisplayName = u.DisplayName,
                                })
                                .OrderByDescending(u => u.Id)
                                .ToList();
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}