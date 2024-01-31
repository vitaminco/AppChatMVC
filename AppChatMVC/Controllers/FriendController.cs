using AppChatMVC.Entities;
using AppChatMVC.ViewModels.Account;
using AppChatMVC.ViewModels.Friend;
using AppChatMVC.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using NuGet.Protocol.Plugins;
using System.Security.Claims;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace AppChatMVC.Controllers
{
    public class FriendController : ChattingAppControllerBase
    {
        public FriendController(AppChatDbContext db) : base(db)
        {
        }

        public IActionResult GetAll()
        {

            var currentUserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var myFriendId = _db.AppFriends.Where(f => f.OwnerId == currentUserId)
                .Select(f => f.FriendId)
                .ToList();
            var user = _db.AppUsers
                .Where(u => u.Id != currentUserId)
                .Where(u => myFriendId.Contains(u.Id) == true)
                .Select(p => new FriendListItemVM
                {
                    Id = p.Id,
                    Username = p.Username,
                    DisplayName = p.DisplayName,
                    Avatar = p.Avatar,

                }).OrderByDescending(p => p.Id)
                  .ToList();

            return Ok(myFriendId);
        }

        public IActionResult SearchAll(string keyword = "")
        {
            var data = _db.AppUsers.Where(p => p.DisplayName.Contains(keyword) || p.Username.Contains(keyword))
                                      .Select(p => new UserListItemVM
                                      {
                                          Id = p.Id,
                                          Username = p.Username,
                                          DisplayName = p.DisplayName,
                                          Avatar = p.Avatar,

                                      }).OrderByDescending(p => p.Id)
                                        .ToList();
            return Ok(data);
        }

        public IActionResult SearchFriend(string keyword = "")
        {
            var currentUserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var myFriendId = _db.AppFriends.Where(f => f.OwnerId == currentUserId)
                .Select(f => f.FriendId)
                .ToList();
            var user = _db.AppUsers
                .Where(u => u.Id != currentUserId || (!string.IsNullOrEmpty(keyword) && u.Username.Contains(keyword)))
                .Where(u => myFriendId.Contains(u.Id) == false)
                .Select(p => new FriendListItemVM
                {
                    Id = p.Id,
                    Username = p.Username,
                    DisplayName = p.DisplayName,
                    Avatar = p.Avatar,

                }).OrderByDescending(p => p.Id)
                  .ToList();
            return Ok(user);
        }
    }
}
