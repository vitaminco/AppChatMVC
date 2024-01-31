
using AppChatMVC.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;
using System.Reflection;
using System.Security.Claims;

namespace AppChatMVC.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        protected int CurrentUserId
        {
            get => Convert.ToInt32(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        AppChatDbContext _db;
        public ChatHub(AppChatDbContext db)
        {
            _db = db;
        }
        public void SendMessage(string message)
        {
            //lấy thông tin người gửi
            var sender = Context.User.FindFirstValue(ClaimTypes.Name);
            //gửi đến tất cả user
            Clients.All.SendAsync("ReceiveMessage", sender, message, DateTime.Now).Wait();
            //gửi đến 1 user nào đó(tự mò)

            /* var userId = "10";
             await Clients.User(userId).SendAsync("");*/
        }

        public void AddFriend(int id)
        {
            var exists = _db.AppAddFriendTickets.Any(t => t.SenderId == CurrentUserId && t.TargetId == id && t.IsAccept == false);
            if (exists)
            {
                return;
            }

            var ticket = new AppAddFriendTicket
            {
                IsAccept = false,
                Message = "Kết bạn",
                SendAt = DateTime.UtcNow.AddHours(7),
                SenderId = CurrentUserId,
                TargetId = id
            };
            _db.Add(ticket);
            _db.SaveChanges();

            //Tạo thông báo đến target user
            Clients.User(id.ToString()).SendAsync("ReceiveAddFriendTicket", 1);

        }

    }
}
