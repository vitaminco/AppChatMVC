using Microsoft.AspNetCore.Http;

namespace AppChatMVC.Common
{
    public static class AppExtention
    {
        public static void SetUserId(this HttpContext context, int userId)
        {
            context.Session.SetInt32("userId", userId);
        }
        public static int? GetUserId(this HttpContext context)
        {
            return context.Session.GetInt32("userId");
        }

        public static void SetUserName(this HttpContext context, string userName)
        {
            context.Session.SetString("userName", userName);
        } 
        public static string GetUserName(this HttpContext context)
        {
            return context.Session.GetString("userName") ?? "";
        }

        public static void SetDislayName(this HttpContext context, string displayName)
        {
            context.Session.SetString("displayName", displayName);
        }
        public static string GetDislayName(this HttpContext context)
        {
            return context.Session.GetString("displayName") ?? "";
        }
    }
}
