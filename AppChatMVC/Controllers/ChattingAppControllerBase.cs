using AppChatMVC.Common;
using AppChatMVC.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AppChatMVC.Controllers
{
    public class ChattingAppControllerBase : Controller
    {
        protected readonly AppChatDbContext _db;

        public ChattingAppControllerBase(AppChatDbContext db)
        {
            _db = db;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.GetUserId() == null && context.HttpContext.GetRouteValue("Controller").ToString() !="Account")
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
                return;
            }
        }

        protected void SetSuccessMesg(string msg)
        {
            TempData["_SuccessMesg"] = msg;
        }
        protected void SetErrorMesg(string msg)
        {
            TempData["_ErrorMesg"] = msg;
        }
    }
}
